using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace snakegame
{
    internal class Obstacle : GameObject
    {
        public Obstacle(int x, int y, int size) { X = x; Y = y; Size = size; }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.SlateGray, X, Y, Size, Size);
            g.DrawRectangle(Pens.DimGray, X, Y, Size - 1, Size - 1);
        }
    }
}
