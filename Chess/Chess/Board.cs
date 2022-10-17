using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Board
    {
        const int length = 8;
        const int width = 8;

        public Tile[,] Tiles
        {
            get;
            private set;
        }

        public Board()
        {
            Tiles = new Tile[length, width];
            Fill();
        }
        void Fill()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tiles[i, j] = new Tile();
                    Tiles[i, j].Position.X = j;
                    Tiles[i, j].Position.Y = i;
                }
            }
        }

        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < length; i++)
            {
                int rowNumber = 8 - i;
                Console.Write(rowNumber + " ");

                for (int j = 0; j < width; j++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write("[");
                    Console.Write(Tiles[i, j].getTileIcon());
                    Console.Write("]");
                }
                Console.Write("\n");
            }
            
            string columnLabels = "   a  b  c  d  e  f  g  h";
            Console.WriteLine(columnLabels);
        }
    }
}
