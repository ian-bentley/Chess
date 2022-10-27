using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Board
    {
        public int Length 
        {
            get { return 8; }
        }

        public int Width
        {
            get { return 8; }
        }

        public Tile[,] Tiles
        {
            get;
            private set;
        }

        public Board()
        {
            Tiles = new Tile[Length, Width];
            Fill();
        }
        void Fill()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tiles[i, j] = new Tile();
                    Tiles[i, j].Position.X = j;
                    Tiles[i, j].Position.Y = i;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < Length; i++)
            {
                int rowNumber = 8 - i;
                Console.Write(rowNumber + " ");

                for (int j = 0; j < Width; j++)
                {
                    Console.Write("[");
                    Console.Write(Tiles[i, j].getTileIcon());
                    Console.Write("]");
                }
                Console.Write("\n");
            }

            string columnLabels = "   a  b  c  d  e  f  g  h";
            Console.WriteLine(columnLabels);
        }

        public Tile tileAt(Position position)
        {
            Tile tile;
            tile = Tiles[position.Y, position.X];
            return tile;
        }

        public Tile tileAt(int x, int y)
        {
            Tile tile;
            tile = Tiles[y, x];
            return tile;
        }

        public bool isInBounds(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
