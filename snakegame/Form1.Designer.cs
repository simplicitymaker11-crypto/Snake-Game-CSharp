namespace snakegame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlGame = new Panel();
            btnReset = new Button();
            btnExit = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pnlTop = new Panel();
            btnPause = new Button();
            lblScore = new Label();
            lblLevel = new Label();
            lblControls = new Label();
            chkWalls = new CheckBox();
            cmbDifficulty = new ComboBox();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlGame
            // 
            pnlGame.BackColor = SystemColors.MenuText;
            pnlGame.Dock = DockStyle.Fill;
            pnlGame.Location = new Point(0, 60);
            pnlGame.Name = "pnlGame";
            pnlGame.Size = new Size(1796, 659);
            pnlGame.TabIndex = 0;
            pnlGame.Paint += pnlGame_Paint;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.DimGray;
            btnReset.Font = new Font("Atiba", 10F);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(12, 10);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(120, 40);
            btnReset.TabIndex = 1;
            btnReset.TabStop = false;
            btnReset.Text = "RESET";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.DimGray;
            btnExit.Font = new Font("Atiba", 10F);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(140, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 40);
            btnExit.TabIndex = 2;
            btnExit.TabStop = false;
            btnExit.Text = "EXIT";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // timer1
            // 
            timer1.Interval = 150;
            timer1.Tick += timer1_Tick;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(30, 30, 30);
            pnlTop.Controls.Add(btnReset);
            pnlTop.Controls.Add(btnExit);
            pnlTop.Controls.Add(btnPause);
            pnlTop.Controls.Add(lblScore);
            pnlTop.Controls.Add(lblLevel);
            pnlTop.Controls.Add(cmbDifficulty);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1796, 60);
            pnlTop.TabIndex = 1;
            // 
            // btnPause
            // 
            btnPause.BackColor = Color.DimGray;
            btnPause.Font = new Font("Atiba", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(268, 10);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(120, 40);
            btnPause.TabIndex = 3;
            btnPause.TabStop = false;
            btnPause.Text = "PAUSE";
            btnPause.UseVisualStyleBackColor = false;
            btnPause.Click += BtnPause_Click;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Agency FB", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScore.ForeColor = Color.White;
            lblScore.Location = new Point(410, 9);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(106, 39);
            lblScore.TabIndex = 3;
            lblScore.Text = "Score: 0";
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Font = new Font("Agency FB", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLevel.ForeColor = Color.Green;
            lblLevel.Location = new Point(1779, 9);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(107, 44);
            lblLevel.TabIndex = 4;
            lblLevel.Text = "Level: 0";
            //
            // lblControls
            //
            lblControls.AutoSize = true;
            lblControls.ForeColor = Color.Gainsboro;
            lblControls.Font = new Font("Segoe UI", 10F);
            lblControls.Location = new Point(560, 20);
            lblControls.Name = "lblControls";
            lblControls.Text = "Controls: Arrow keys or W/A/S/D  |  P = Pause";
            pnlTop.Controls.Add(lblControls);
            // 
            // chkWalls
            // 
            chkWalls.AutoSize = true;
            chkWalls.Checked = true;
            chkWalls.Text = "Walls";
            chkWalls.ForeColor = Color.White;
            chkWalls.Font = new Font("Segoe UI", 11F);
            chkWalls.Location = new Point(1400, 16);
            chkWalls.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkWalls.Name = "chkWalls";
            chkWalls.TabStop = false;
            pnlTop.Controls.Add(chkWalls);
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbDifficulty.BackColor = SystemColors.InactiveCaptionText;
            cmbDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDifficulty.Font = new Font("Agency FB", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDifficulty.ForeColor = SystemColors.ScrollBar;
            cmbDifficulty.Items.AddRange(new object[] { "Easy", "Medium", "Hard", "Expert" });
            cmbDifficulty.Location = new Point(1521, 16);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(120, 32);
            cmbDifficulty.TabIndex = 5;
            cmbDifficulty.TabStop = false;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1796, 719);
            Controls.Add(pnlGame);
            Controls.Add(pnlTop);
            KeyPreview = true;
            Name = "Form1";
            Text = resources.GetString("$this.Text");
            WindowState = FormWindowState.Maximized;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlGame;
        private Button btnReset;
        private Button btnExit;
        private System.Windows.Forms.Timer timer1;

        private Panel pnlTop;
        private Label lblScore;
        private Label lblLevel;
        private Label lblControls;
        private CheckBox chkWalls;
        private ComboBox cmbDifficulty;
        

        private Button btnPause;

    }
    
    }
