using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Board
    {
        const int length = 8;
        const int width = 8;
        Tile[,] tiles = new Tile[length, width];

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
                    tiles[i, j] = new Tile();
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(tiles[i, j].getTileIcon());
                }
                Console.Write("\n");
            }
        }
    }
}
