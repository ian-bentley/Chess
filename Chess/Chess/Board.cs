using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Board
    {
        string tileEmpty = "[ ]";
        const int length = 8;
        const int width = 8;
        string[,] tiles = new string[length, width];

        public Board()
        {
            Fill();
        }
        void Fill()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles[i, j] = tileEmpty;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(tiles[i, j]);
                }
                Console.Write("\n");
            }
        }
    }
}
