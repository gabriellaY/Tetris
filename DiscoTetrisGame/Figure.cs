using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTetrisGame
{
    /// <summary>
    /// Abstract class for tetris figures creation. It contains the main movement methods for a figure.
    /// </summary>
    public abstract class Figure
    {
        protected const int FrameHeight = 25;
        protected const int FrameWidth = 18;

        private List<Block> _blocks;
        private int _x;
        private int _y;

        public Figure(int x, int y, List<Block> blocks)
        {
            X = x;
            Y = y;

            Blocks = blocks != null && blocks.Count == 4
                ? blocks
                : throw new ArgumentNullException("The blocks cannot be null.", nameof(blocks));
        }

        /// <summary>
        /// X coordinate of a figure.
        /// </summary>
        public int X
        {
            get => _x;
            set => _x = value >= 0 && value <= 2 * FrameWidth - 2
                ? value
                : throw new ArgumentException("The X coordinate cannot be negative ot bigger than the console width.", nameof(value));
        }

        /// <summary>
        /// Y coordinate of a figure.
        /// </summary>
        public int Y
        {
            get => _y;
            set => _y = value >= 0 && value <= FrameHeight - 1
                ? value
                : throw new ArgumentException("The Y coordinates cannot be negative ot bigger than the console height.", nameof(value));
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// Deletes figure from the console.
        /// </summary>
        public void ClearFigure()
        {
            foreach (var block in Blocks)
            {
                block.Clear();
            }
        }

        /// <summary>
        /// Moves a figure down.
        /// </summary>
        public void MoveDown()
        {
            ClearFigure();
            X = X;
            Y += 1;
            Display();
        }

        /// <summary>
        /// Moves a figure to the left.
        /// </summary>
        public void MoveLeft()
        {
            ClearFigure();
            X -= 2;
            Y = Y;
            Display();
        }

        /// <summary>
        /// Moves a figure to the right.
        /// </summary>
        public void MoveRight()
        {
            ClearFigure();
            X += 2;
            Y = Y;
            Display();
        }

        /// <summary>
        /// Checks if a figure can move left or right.
        /// </summary>
        public abstract bool CanMove();

        /// <summary>
        /// Checks if a figure is at the bottom of the game frame.
        /// </summary>
        public abstract bool IsAtTheBottom();

        /// <summary>
        /// Displays a figure on the console.
        /// </summary>
        public abstract void Display();
    }
}
