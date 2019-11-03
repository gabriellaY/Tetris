using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTetrisGame
{
    public class J : Figure
    {
        public J(int x, int y, List<Block> blocks) : base(x, y, blocks)
        {

        }

        public override void Display()
        {
            Blocks[0].X = X;
            Blocks[0].Y = Y;

            Blocks[1].X = X;
            Blocks[1].Y = Y + 1;

            Blocks[2].X = X;
            Blocks[2].Y = Y + 2;

            Blocks[3].X = X - 2;
            Blocks[3].Y = Y + 2;

            foreach (var square in Blocks)
            {
                square.Display();
            }
        }

        public override bool CanMove()
        {
            return X > 3 && X < 2*FrameWidth - 4;
        }

        public override bool IsAtTheBottom()
        {
            return Y < 0 || Y > FrameHeight - 4;
        }
    }
}

