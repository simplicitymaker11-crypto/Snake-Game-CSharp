using System.Drawing.Drawing2D;

namespace snakegame
{
    public partial class Form1 : Form
    {
        private Snake snake = new Snake(5);
        private Food food = new Food();
        private mainform? mainForm;
        private bool gameOver = false;
        private bool isPaused = false;
        private int score = 0;

        private void TogglePause()
        {
            isPaused = !isPaused;

            if (isPaused) timer1.Stop();
            else timer1.Start();

            btnPause.Text = isPaused ? "RESUME" : "PAUSE"; 
            ActiveControl = null;      // keep Space/Enter from re-clicking the button
            pnlGame.Invalidate();      // redraw so the PAUSED text appears or disappears
        }

        private void BtnPause_Click(object sender, EventArgs e) => TogglePause();
        private void UpdateLevel()    // updates the level label and its colour only
        {
            int level = CurrentLevel();
            lblLevel.Text = "Level: " + level;
            lblLevel.ForeColor = ((SolidBrush)GetSnakeColors().body).Color;
        }


        public Form1() : this(null)
        {
           
        }

        public Form1(mainform? mainFormReference) // Constructor that accepts a reference to the main form
        {
            mainForm = mainFormReference;// Store the reference to the main form
            InitializeComponent();
            cmbDifficulty.SelectedIndex = 0;
            ApplySpeed();

            chkWalls.CheckedChanged += (_, __) => ActiveControl = null;
            this.FormClosing += (_, __) =>
            {
                mainForm?.UpdateHighScoreFromGame(score);
                mainForm?.Show();
            }; // Update high score when the game form is closing




            UpdateLevel();
            timer1.Start();

            this.Shown += (_, __) => // When the form is shown, spawn food and invalidate the game panel to trigger a repaint
            {
                SpawnFood();
                pnlGame.Invalidate();
                this.BeginInvoke(new Action(() => this.Activate()));
            };
        
    
        }

        private bool SpawnFood()
        {
            int gridSize = snake.rect_size;

            int cols = pnlGame.ClientSize.Width / gridSize;
            int rows = pnlGame.ClientSize.Height / gridSize;

            for (int attempt = 0; attempt < 500; attempt++)
            {
                food.SpawnFood(cols, rows);

                if (!IsFoodOnSnake())
                {
                    return true;
                }
            }

            return false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)// Override to handle key presses for snake movement and pause functionality
        {
            if (keyData == Keys.P) { TogglePause(); return true; }

            switch (keyData)
            {

                case Keys.Up: case Keys.W: if (!isPaused) snake.changeDirection(0); return true;
                case Keys.Right: case Keys.D: if (!isPaused) snake.changeDirection(1); return true;
                case Keys.Down: case Keys.S: if (!isPaused) snake.changeDirection(2); return true;
                case Keys.Left: case Keys.A: if (!isPaused) snake.changeDirection(3); return true;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void timer1_Tick(object sender, EventArgs e) // Game loop tick event handler
        {
            if (gameOver) return;

            snake.Move(pnlGame.Width, pnlGame.Height, !chkWalls.Checked);
            if (chkWalls.Checked && snake.CheckWallCollision(pnlGame.Width, pnlGame.Height))
            {
                EndGame("You hit the wall!");
                return;
            }

            if (snake.CheckFoodCollision(food))
            {
                score += food.points;
                lblScore.Text = "Score: " + score;
                snake.Grow();
                if (!SpawnFood())
                {
                    EndGame("Unable to find space for food!");
                    return;
                }

                UpdateLevel(); // Update the level display
            }

            if (snake.CheckSelfCollision())
            {
               
                EndGame("You hit yourself!");
                return;
            }

            pnlGame.Invalidate();
        }

        /*private void EndGame(string message)// End the game, stop the timer, show a message box, and update the high score if necessary
        {
            timer1.Stop();
            MessageBox.Show(message + "\nFinal Score: " + score, "GAME OVER!");

            if (mainForm != null)
            {
                mainForm.UpdateHighScoreFromGame(score);
                mainForm.Show();
            }

            this.Close();
        }*/
        private void EndGame(string message)
        {
            timer1.Stop();
            mainForm?.UpdateHighScoreFromGame(score);
            var r = MessageBox.Show(message + "\nFinal Score: " + score + "\n\nPlay again?",
                                    "GAME OVER!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes) ResetGame();
            else Close();
        }
        private void ResetGame()
        {
            if (isPaused) TogglePause();
            score = 0;
            lblScore.Text = "Score: 0";
            snake = new Snake(5);
            food = new Food();
            SpawnFood();
            timer1.Start();
            ActiveControl = null;
            UpdateLevel();
            pnlGame.Invalidate();
        }

        private void BtnReset_Click(object sender, EventArgs e)// Reset the game state, score, and snake position
        {
            mainForm?.UpdateHighScoreFromGame(score);
            ResetGame();
        }


        private void BtnExit_Click(object sender, EventArgs e)// Exit the game, stop the timer, show the main form if available, and close the game form
        {
            timer1.Stop();
            var result = MessageBox.Show(
        "Do you want to exit the game?\nYour score: " + score,
        "Exit Game",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();                    // FormClosing saves the high score
            }
            else if (!isPaused)
            {
                timer1.Start();                  // "No": keep playing
            }
        }

        /// <summary>
        /// Gets the colors for the snake based on its current level.
        /// </summary>
        private static readonly (Brush body, Brush head)[] snakeColors =
{
    (Brushes.Green,       Brushes.Orange),       // level 0
    (Brushes.LimeGreen,   Brushes.DarkGreen),    // 1
    (Brushes.YellowGreen, Brushes.Teal),         // 2
    (Brushes.Gold,        Brushes.SaddleBrown),  // 3
    (Brushes.Orange,      Brushes.Maroon),       // 4
    (Brushes.OrangeRed,   Brushes.Gold),         // 5
    (Brushes.Red,         Brushes.Purple),       // 6
    (Brushes.HotPink,     Brushes.DarkMagenta),  // 7
    (Brushes.Violet,      Brushes.Blue),         // 8
    (Brushes.DeepSkyBlue, Brushes.Purple),       // 9
    (Brushes.Cyan,        Brushes.Crimson),      // 10
    (Brushes.Aquamarine,       Brushes.Red),          // 11
    (Brushes.Gold,        Brushes.Crimson)       // 12
};

        private int CurrentLevel() => Math.Min(score / 100, 12);// Calculate the current level based on the score, capped at level 12

        private (Brush body, Brush head) GetSnakeColors() => snakeColors[CurrentLevel()];// Get the colors for the snake based on its current level


        

        private readonly int[] difficultyIntervals = { 200, 150, 100, 60 };   // Easy, Medium, Hard, Expert

        private void ApplySpeed()
        {
            int i = Math.Clamp(cmbDifficulty.SelectedIndex, 0, difficultyIntervals.Length - 1);
            timer1.Interval = difficultyIntervals[i];
        }
        private void cmbDifficulty_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplySpeed();
            ActiveControl = null;      // keep the arrow keys working after choosing
        }

        private bool IsFoodOnSnake()
        {
            Rectangle foodRect = new Rectangle(
                food.X, food.Y, food.size, food.size
            );

            foreach (Point segment in snake.body)
            {
                Rectangle snakeRect = new Rectangle(
                    segment.X,
                    segment.Y,
                    snake.rect_size,
                    snake.rect_size
                );

                if (foodRect.IntersectsWith(snakeRect))
                {
                    return true;
                }
            }

            return false;
        }
        private void pnlGame_Paint(object sender, PaintEventArgs e)// Paint event handler for the game panel, responsible for drawing the snake, food, and paused state
        {
            int s = snake.rect_size;
            var (bodyBrush, headBrush) = GetSnakeColors();

            e.Graphics.FillEllipse(food.color, food.X, food.Y, food.size, food.size);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 1; i < snake.body.Count; i++)
            {
                DrawRoundedSegment(
                    e.Graphics,
                    bodyBrush,
                    snake.body[i],
                    s
                );
            }

            // head
            Point h = snake.body[0];

            DrawRoundedSegment(
                e.Graphics,
                headBrush,
                h,
                s
            );

            // eyes: white, because most head colors are dark
            Point eye1, eye2;
            switch (snake.direction)
            {
                case 0: // Up
                    eye1 = new Point(h.X + 10, h.Y + 8);
                    eye2 = new Point(h.X + 24, h.Y + 8);
                    break;

                case 2: // Down
                    eye1 = new Point(h.X + 10, h.Y + 24);
                    eye2 = new Point(h.X + 24, h.Y + 24);
                    break;

                case 3: // Left
                    eye1 = new Point(h.X + 8, h.Y + 10);
                    eye2 = new Point(h.X + 8, h.Y + 24);
                    break;

                default: // Right
                    eye1 = new Point(h.X + 24, h.Y + 10);
                    eye2 = new Point(h.X + 24, h.Y + 24);
                    break;
            }
            e.Graphics.FillEllipse(Brushes.White, eye1.X, eye1.Y, 5, 5);
            e.Graphics.FillEllipse(Brushes.White, eye2.X, eye2.Y, 5, 5);

            if (isPaused) // Draw the paused message in the center of the game panel
            {
                using var font = new Font("Agency FB", 40F, FontStyle.Bold);
                var center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("PAUSED - press P to resume", font, Brushes.Red, pnlGame.ClientRectangle, center);
            }
        }
        private void DrawRoundedSegment(Graphics g, Brush brush, Point p, int s)
        {
            int gap = 4;
            int size = s - gap;

            Rectangle rect = new Rectangle(
                p.X + gap / 2,
                p.Y + gap / 2,
                size,
                size
            );

            using var path = new GraphicsPath();
            path.AddRoundedRectangle(rect, new Size(5, 5));

            g.FillPath(brush, path);
        }




    }


    }


