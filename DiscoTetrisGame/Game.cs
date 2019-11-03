using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace DiscoTetrisGame
{
    /// <summary>
    /// Contains the game logic and design.
    /// </summary>
    public class Game
    {
        private readonly int FrameHeight = 30;
        private readonly int FrameWidth = 25;

        private int _level;
        private int _score;
        //private Figure _next;

        /// <summary>
        /// Sets level and score to zero.
        /// </summary>
        public Game()
        {
            Level = 0;
            Score = 0;
        }

        public int Level
        {
            get => _level;
            set => _level = value >= 0
                ? value
                : throw new ArgumentException("The score should be positive number or zero.", nameof(value));
        }

        public int Score
        {
            get => _score;
            set => _score = value >= 0
                ? value
                : throw new ArgumentException("The score should be positive number or zero.", nameof(value));
        }

        public Figure Next { get; set; }

        private static ConsoleColor GenerateRandomColor()
        {
            Random r = new Random();
            int colorNumber = r.Next(1, 10);

            switch (colorNumber)
            {
                case 1: return ConsoleColor.Red;
                case 2: return ConsoleColor.Green;
                case 3: return ConsoleColor.Yellow;
                case 4: return ConsoleColor.Blue;
                case 5: return ConsoleColor.Cyan;
                case 6: return ConsoleColor.Magenta;
                case 7: return ConsoleColor.DarkGray;
                case 8: return ConsoleColor.White;
                case 9: return ConsoleColor.DarkYellow;
                case 10: return ConsoleColor.DarkGreen;
                default: return ConsoleColor.Green;
            }
        }

        private static void ChangeColors(Figure figure)
        {
            figure.Blocks[0].Colour = GenerateRandomColor();
            figure.Blocks[1].Colour = GenerateRandomColor();
            figure.Blocks[2].Colour = GenerateRandomColor();
            figure.Blocks[3].Colour = GenerateRandomColor();
        }

        private static Figure CreateRandomFigure()
        {
            List<Block> blocks = new List<Block>();

            var colour = GenerateRandomColor();
            blocks.Add(new Block(10, 25, colour));

            colour = Console.BackgroundColor = ConsoleColor.Blue;
            blocks.Add(new Block(10, 25, colour));

            colour = Console.BackgroundColor = ConsoleColor.Yellow;
            blocks.Add(new Block(10, 25, colour));

            colour = Console.BackgroundColor = ConsoleColor.Green;
            blocks.Add(new Block(10, 25, colour));

            Random r = new Random();
            int randomNumber = r.Next(1, 7);

            switch (randomNumber)
            {
                case 1: return new I(15, 1, blocks);
                case 2: return new S(15, 1, blocks);
                case 3: return new L(15, 1, blocks);
                case 4: return new J(15, 1, blocks);
                case 5: return new O(15, 1, blocks);
                case 6: return new T(15, 1, blocks);
                case 7: return new Z(15, 1, blocks);

                default: return new I(15, 0, blocks);
            }
        }

        private bool AreColided(Figure old, Figure newF)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (old.Blocks[i].X == newF.Blocks[j].X && old.Blocks[i].Y == newF.Blocks[j].Y + 1)
                    {
                        return true;
                    }
                }
            }            
            return false;
        }

        private bool AreThereCollisions(List<Figure> figures, Figure newFigure)
        {
            foreach (var fig in figures)
            {
                if (AreColided(fig, newFigure))
                {
                    return true;
                }
            }
            return false;
        }

        private void ClearFullLine(List<Figure> figures, int line)
        {
            foreach (var figure in figures)
            {
                var blocks = figure.Blocks;

                foreach (var block in blocks)
                {
                    if (block.Y == line)
                    {
                        block.Clear();
                    }
                }
            }

            foreach (var fig in figures)
            {
                if (fig.Y >= line)
                {
                    fig.MoveDown();
                }
            }
        }

        private void CheckForFullLines(List<Figure> figures)
        {
            bool hasFullLine = false;
            for (int row = FrameHeight - 2; row > 0; row--)
            {
                var figuresInRow = figures.Where(figure => figure.Blocks.Any(block => block.Y == row));

                int count = 0;
                foreach (var figure in figuresInRow)
                {
                    count += figure.Blocks.Where(block => block.Y == row).Count();
                }

                hasFullLine = count >= 17;
                if (hasFullLine)
                {
                    ClearFullLine(figures, row);
                    break;
                }
            }
        }

        private bool IsOver(List<Figure> figures)
        {
            foreach (var fig in figures)
            {
                if (fig.Y <= 1)
                {
                    return true;
                }
            }
            return false;
        }

        private void EndGame()
        {
            Console.Clear();
            var frame = new Frame();
            frame.CreateGameFrame();

            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.SetCursorPosition(FrameHeight / 2, FrameWidth / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GAME OVER!");
            Console.ResetColor();

            Console.SetCursorPosition(left, top);
        }

        private bool NewGame()
        {
            Console.WriteLine("To start new game press ENTER.");
            Console.WriteLine("To quit the game press ESC.");

            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                return true;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return false;
            }

            throw new InvalidOperationException("Invalid button pressed.");
        }

        private void UpdateScore()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(57, 7);
            Console.Write(Score);
        }

        private void UpdateLevel()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(57, 5);
            Console.Write(Level);
        }

        private void StartNewGame()
        {
            Console.CursorVisible = false;

            Frame frame = new Frame();
            frame.CreateGameFrame();

            UpdateScore();
            UpdateLevel();

            List<Figure> figures = new List<Figure>();

            while (true)
            {
                Figure newFigure = CreateRandomFigure();
                newFigure.Display();

                while (!newFigure.IsAtTheBottom())
                {
                    if (AreThereCollisions(figures, newFigure))
                    {
                        break;
                    }

                    newFigure.MoveDown();

                    ChangeColors(newFigure);

                    Thread.Sleep(200);

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            if (newFigure.CanMove())
                            {
                                if (AreThereCollisions(figures, newFigure))
                                {
                                    break;
                                }

                                newFigure.MoveRight();
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            if (newFigure.CanMove())
                            {
                                if (AreThereCollisions(figures, newFigure))
                                {
                                    break;
                                }

                                newFigure.MoveLeft();
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.DownArrow)
                        {
                            if (AreThereCollisions(figures, newFigure))
                            {
                                break;
                            }

                            if (!newFigure.IsAtTheBottom())
                            {
                                newFigure.MoveDown();
                            }
                        }
                    }
                }

                figures.Add(newFigure);

                Score += 10;
                UpdateScore();

                if (IsOver(figures))
                {
                    EndGame();
                    break;
                }

                CheckForFullLines(figures);
            }

            if (NewGame())
            {
                Level = 0;
                Score = 0;
                Start();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void Start()
        {
            Console.Clear();
            StartNewGame();
        }
    }
}
