using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace snakegame
{
    public partial class mainform : Form 
    {
        private int highScore = 0;

        // Define the path for the high score file in the application directory
        private static readonly string HighScorePath =
               Path.Combine(AppContext.BaseDirectory, "highscore.txt");

        public mainform()
        {
            InitializeComponent();
            LoadHighScore();
            UpdateHighScoreLabel();
            button1.Click += Button1_Click;// Attach the event handler for button1 click

        }

        // Define a set of colors for the button
        private readonly Color[] buttonColors =
{
    Color.FromArgb(120, 144, 156),   // blue grey
    Color.FromArgb(129, 150, 138),   // sage
    Color.FromArgb(158, 148, 134),   // warm taupe
    Color.FromArgb(140, 140, 160),   // dusty lavender
    Color.FromArgb(150, 130, 130),   // muted rose
    Color.FromArgb(128, 150, 150),   // soft teal
    Color.FromArgb(155, 155, 145)
};
        private int colorIndex = 0; // Index to track the current color of the button
        private void LoadHighScore()// Load the high score from the file if it exists
        {
            try
            {
                if (File.Exists(HighScorePath))
                {
                    string score = File.ReadAllText(HighScorePath);
                    if (int.TryParse(score, out int loadedScore))
                    {
                        highScore = loadedScore;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading high score: " + ex.Message);
            }
        }



        private void SaveHighScore()// Save the high score to the file
        {
            try
            {
                File.WriteAllText(HighScorePath, highScore.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving high score: " + ex.Message);
            }
        }

        private void UpdateHighScoreLabel()// Update the label to display the current high score
        {
            label1.Text = "HIGHEST: " + highScore;
        }

        public void UpdateHighScoreFromGame(int gameScore)// Update the high score if the game score is higher and save it
        {
            if (gameScore > highScore)
            {
                highScore = gameScore;
                SaveHighScore();
                UpdateHighScoreLabel();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(this);

            gameForm.Show();

            this.Hide();// Hide the main form when the game starts
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // This method is intentionally left empty to avoid duplicate event handling for button1 click
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorIndex = (colorIndex + 1) % buttonColors.Length;
            button2.BackColor = buttonColors[colorIndex];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //This method is intentionally left empty to avoid duplicate event handling 
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //This method is intentionally left empty to avoid duplicate event handling 
        }
    }
}
