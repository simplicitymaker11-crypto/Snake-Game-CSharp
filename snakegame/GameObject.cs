using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace snakegame
{
    internal abstract class GameObject
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Size { get; protected set; }
        public Rectangle Bounds => new Rectangle(X, Y, Size, Size);

        public abstract void Draw(Graphics g);      // each item draws itself
    }
}
