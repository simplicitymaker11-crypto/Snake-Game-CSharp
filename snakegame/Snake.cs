using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace snakegame
{
    internal class Snake
    {
        
        public List<Point> body { get; set; }
        public int direction { get; set; }
        private int nextDirection;


        public int rect_size = 40;
        public Snake(int snakelength)
        {

            body = new List<Point>();
            direction = 1;  // Start moving right
            nextDirection = 1;
            int startX = (400 / rect_size) * rect_size; // Calculate the starting X position based on the rectangle size
            int startY = (300 / rect_size) * rect_size; // Calculate the starting Y position based on the rectangle size
            for (int i = 0; i < snakelength; i++)
            {
                body.Add(new Point(startX - i * rect_size, startY));
            }

        }


        public void Move(int width, int height, bool wrap)// Move the snake in the current direction and wrap around the edges of the game area
        {
            direction = nextDirection;  // Update direction to the next direction

            Point head = body[0];
            Point newHead = head;
            switch (direction)
            {
                case 0: // Up
                    newHead = new Point(head.X, head.Y - rect_size);
                    break;
                case 1: // Right
                    newHead = new Point(head.X + rect_size, head.Y);
                    break;
                case 2: // Down
                    newHead = new Point(head.X, head.Y + rect_size);
                    break;
                case 3: // Left
                    newHead = new Point(head.X - rect_size, head.Y);
                    break;
            }

            if (wrap)
            {
                int cols = width / rect_size;
                int rows = height / rect_size;
                newHead.X = ((newHead.X / rect_size + cols) % cols) * rect_size;
                newHead.Y = ((newHead.Y / rect_size + rows) % rows) * rect_size;
            }

            body.Insert(0, newHead);
            body.RemoveAt(body.Count - 1);
        }

        public void Grow()
        {
            body.Add(body[^1]);// Add a new segment at the tail (last segment)
        }

        public void changeDirection(int newDirection)
        {
            // compare with the direction the snake last MOVED in
            if (Math.Abs(newDirection - direction) != 2)
            {
                nextDirection = newDirection;
            }
        }

        // Check if snake eats food
        public bool CheckFoodCollision(Food food)
        {
            Point head = body[0];
            var headRect = new Rectangle(head.X, head.Y, rect_size, rect_size);
            return headRect.IntersectsWith(food.Bounds);
        }

        // Check if snake hits itself
        public bool CheckSelfCollision()
        {
            Point head = body[0];
            for (int i = 1; i < body.Count; i++)
            {
                if (head.X == body[i].X && head.Y == body[i].Y)
                {
                    return true;  // Snake hit itself
                }
            }
            return false;
        }

        // Check if snake hits wall
        public bool CheckWallCollision(int width, int height)
        {
            Point head = body[0];
            int cols = width / rect_size, rows = height / rect_size;
            return head.X < 0 || head.Y < 0 || head.X >= cols * rect_size || head.Y >= rows * rect_size;
        }
    }
}
