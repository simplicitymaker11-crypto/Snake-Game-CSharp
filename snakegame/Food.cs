using System;
using System.Drawing;

namespace snakegame
{
    internal class Food
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int size = 20;
        public int points = 10;
        public Brush color = Brushes.Red;
        private Random random = new Random();// Random instance for generating food positions and types

        public void SpawnFood(int cols, int rows)// Spawn food at a random position with a random type based on defined probabilities
        {
            int roll = random.Next(100);
            if (roll < 50) { size = 20; points = 10; color = Brushes.Red; }     // 50%
            else if (roll < 75) { size = 20; points = 15; color = Brushes.Orange; }  // 25%
            else if (roll < 90) { size = 40; points = 20; color = Brushes.Cyan; }    // 15%
            else { size = 60; points = 30; color = Brushes.Gold; }    // 10%

            // Calculate the number of cells the food occupies based on its size
            int cells = size / 20;
            X = random.Next(0, cols - cells + 1) * 20;
            Y = random.Next(0, rows - cells + 1) * 20;
        }
    }
}
