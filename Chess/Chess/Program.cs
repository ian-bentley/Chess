using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player(board);
            board.Draw();
        }
    }
}
