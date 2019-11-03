using System;
using System.Collections.Generic;
using System.Threading;

namespace DiscoTetrisGame
{
    class Program
    {
        static void Main()
        {
            Game newGame = new Game();

            newGame.Start();           
        }
    }
}
