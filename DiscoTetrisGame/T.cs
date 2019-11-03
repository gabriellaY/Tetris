using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTetrisGame
{
    public class T : Figure
    {

        public T(int x, int y, List<Block> blocks) : base(x, y, blocks)
        {

        }

        public override void Display()
        {
            Blocks[0].X = X;
            Blocks[0].Y = Y;

            Blocks[1].X = X + 2;
            Blocks[1].Y = Y;

            Blocks[2].X = X + 4;
            Blocks[2].Y = Y;

            Blocks[3].X = X + 2;
            Blocks[3].Y = Y + 1;

            foreach (var square in Blocks)
            {
                square.Display();
            }
        }

        public override bool CanMove()
        {
            return X > 1 && X < 2 * FrameWidth - 8;
        }

        public override bool IsAtTheBottom()
        {
            return Y < 0 || Y > FrameHeight - 3;
        }
    }
}

