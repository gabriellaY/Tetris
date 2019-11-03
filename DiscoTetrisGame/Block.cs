using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTetrisGame
{
    /// <summary>
    /// Creates one block of each figure.
    /// </summary>
    public class Block
    {
        private const int FrameHeight = 25;
        private const int FrameWidth = 18;

        private readonly string _value = "  ";
        private int _x;
        private int _y;

        public Block(int x, int y, ConsoleColor colour)
        {
            X = x;
            Y = y;
            Colour = colour;
        }

        public int X
        {
            get => _x;
            set => _x = value >= 0 && value < 2 * FrameWidth
                ? value
                : throw new ArgumentException("The coordinates should be positive or zero or less than frame width.", nameof(value));
        }

        public int Y
        {
            get => _y;
            set => _y = value >= 0 && value <= FrameHeight
                ? value
                : throw new ArgumentException("The coordinates should be positive or zero or less than frame height.", nameof(value));
        }

        public ConsoleColor Colour { get; set; }

        /// <summary>
        /// Displays one block of each figure.
        /// </summary>
        public void Display()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.SetCursorPosition(X, Y);
            Console.BackgroundColor = Colour;
            Console.Write(_value);

            Console.ResetColor();
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Deletes a block.
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("  ");
        }
    }
}
