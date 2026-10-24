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


            this.FormClosing += (_, __) => mainForm?.UpdateHighScoreFromGame(score); // Update high score when the game form is closing




            UpdateLevel();
            timer1.Start();

            this.Shown += (_, __) => // When the form is shown, spawn food and invalidate the game panel to trigger a repaint
            {
                SpawnFood();
                pnlGame.Invalidate();
                this.BeginInvoke(new Action(() => this.Activate()));
            };
        
    
        }

        private void SpawnFood() => food.SpawnFood(pnlGame.Width / 20, pnlGame.Height / 20);// Spawn food within the grid based on the panel size
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)// Override to handle key presses for snake movement and pause functionality
        {
            if (keyData == Keys.P) { TogglePause(); return true; }

            switch (keyData)
            {
                case Keys.Up: if (!isPaused) snake.changeDirection(0); return true;
                case Keys.Right: if (!isPaused) snake.changeDirection(1); return true;
                case Keys.Down: if (!isPaused) snake.changeDirection(2); return true;
                case Keys.Left: if (!isPaused) snake.changeDirection(3); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void timer1_Tick(object sender, EventArgs e) // Game loop tick event handler
        {
            if (gameOver) return;

            snake.Move(pnlGame.Width, pnlGame.Height);

            if (snake.CheckFoodCollision(food))
            {
                score += food.points;
                lblScore.Text = "Score: " + score;
                snake.Grow();
                SpawnFood();
               
                UpdateLevel(); // Update the level display
            }

            if (snake.CheckSelfCollision())
            {
               
                EndGame("Game Over! You hit yourself!");
                return;
            }

            pnlGame.Invalidate();
        }

        private void EndGame(string message)// End the game, stop the timer, show a message box, and update the high score if necessary
        {
            timer1.Stop();
            MessageBox.Show(message + "\nFinal Score: " + score);

            if (mainForm != null)
            {
                mainForm.UpdateHighScoreFromGame(score);
                mainForm.Show();
            }

            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)// Reset the game state, score, and snake position
        {
            if (isPaused) TogglePause();
            gameOver = false;

            score = 0;
            lblScore.Text = "Score: 0";

            snake = new Snake(5);
            food = new Food();
            SpawnFood();
            timer1.Start();

            this.ActiveControl = null;
            this.Focus();// Ensure the form has focus to receive key events

            pnlGame.Invalidate();
            UpdateLevel();
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
                mainForm?.Show();                // back to the main menu
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


        private (string name, int interval) GetDifficulty(int level) => level switch // Determine the difficulty name and timer interval based on the current level
        {
            <= 2 => ("Easy", 200),
            <= 5 => ("Medium", 150),
            <= 8 => ("Hard", 110),
            _ => ("Expert", 75)
        };

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
        private void pnlGame_Paint(object sender, PaintEventArgs e)// Paint event handler for the game panel, responsible for drawing the snake, food, and paused state
        {
            int s = snake.rect_size;
            var (bodyBrush, headBrush) = GetSnakeColors();

            e.Graphics.FillRectangle(food.color, food.X, food.Y, food.size, food.size);

            // body: skip index 0 (the head)
            for (int i = 1; i < snake.body.Count; i++)
            {
                e.Graphics.FillRectangle(bodyBrush, snake.body[i].X, snake.body[i].Y, s, s);
            }

            // head
            Point h = snake.body[0];
            var headRect = new Rectangle(h.X, h.Y, s, s);
            headRect.Inflate(4, 4);
            e.Graphics.FillRectangle(headBrush, headRect);

            // eyes: white, because most head colors are dark
            Point eye1, eye2;
            switch (snake.direction)
            {
                case 0: eye1 = new Point(h.X + 2, h.Y - 2); eye2 = new Point(h.X + 14, h.Y - 2); break;
                case 2: eye1 = new Point(h.X + 2, h.Y + 16); eye2 = new Point(h.X + 14, h.Y + 16); break;
                case 3: eye1 = new Point(h.X - 2, h.Y + 2); eye2 = new Point(h.X - 2, h.Y + 14); break;
                default: eye1 = new Point(h.X + 16, h.Y + 2); eye2 = new Point(h.X + 16, h.Y + 14); break;
            }
            e.Graphics.FillEllipse(Brushes.White, eye1.X, eye1.Y, 5, 5);
            e.Graphics.FillEllipse(Brushes.White, eye2.X, eye2.Y, 5, 5);

            if (isPaused) // Draw the paused message in the center of the game panel
            {
                using var font = new Font("Agecy FB", 40F, FontStyle.Bold);
                var center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("PAUSED - press P to resume", font, Brushes.Red, pnlGame.ClientRectangle, center);
            }
        }

    }


    }


