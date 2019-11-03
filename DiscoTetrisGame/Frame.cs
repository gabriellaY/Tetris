using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTetrisGame
{
    public class Frame
    {
        private const int FrameHeight = 25;
        private const int FrameWidth = 18;

        /// <summary>
        /// Creates frame for the game.
        /// </summary>
        public void CreateGameFrame()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.SetCursorPosition(50, 5);
            Console.Write("Level: ");

            Console.SetCursorPosition(50, 7);
            Console.Write("Score: ");

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < FrameHeight; i++)
            {
                Console.Write("*");
                for (int j = 1; j < FrameWidth; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine(" *");
            }

            for (int i = 0; i < FrameWidth - 1; i++)
            {
                Console.Write("* ");
            }
            Console.WriteLine(" *");

            Console.ResetColor();
        }
    }
}
