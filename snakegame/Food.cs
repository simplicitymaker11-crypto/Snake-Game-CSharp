using System.Drawing;

namespace snakegame
{
    internal class Food : GameObject
    {
        private readonly Random random = new Random();
        private Brush color = Brushes.Red;
        public int Points { get; private set; } = 10;

        public Food() { Size = 40; }

        public void SpawnFood(int cols, int rows)
        {
            int roll = random.Next(100);
            if (roll < 50) SetType(20, 10, Brushes.Red);
            else if (roll < 75) SetType(20, 15, Brushes.Orange);
            else if (roll < 90) SetType(40, 20, Brushes.Cyan);
            else SetType(60, 30, Brushes.Gold);

            int cells = (Size + 40 - 1) / 40;
            X = random.Next(0, cols - cells + 1) * 40;
            Y = random.Next(0, rows - cells + 1) * 40;
        }

        private void SetType(int size, int points, Brush brush) { Size = size; Points = points; color = brush; }

        public override void Draw(Graphics g) => g.FillEllipse(color, X, Y, Size, Size);
    }
}