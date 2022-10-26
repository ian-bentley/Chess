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
        public Tile QueenRookStartTile
        {
            get;
            private set;
        }

        public Tile QueenKnightStartTile
        {
            get;
            private set;
        }

        public Tile QueenBishopStartTile
        {
            get;
            private set;
        }

        public Tile QueenStartTile
        {
            get;
            private set;
        }

        public Tile KingStartTile
        {
            get;
            private set;
        }

        public Tile KingBishopStartTile
        {
            get;
            private set;
        }

        public Tile KingKnightStartTile
        {
            get;
            private set;
        }

        public Tile KingRookStartTile
        {
            get;
            private set;
        }

        public Tile APawnStartTile
        {
            get;
            private set;
        }

        public Tile BPawnStartTile
        {
            get;
            private set;
        }

        public Tile CPawnStartTile
        {
            get;
            private set;
        }

        public Tile DPawnStartTile
        {
            get;
            private set;
        }

        public Tile EPawnStartTile
        {
            get;
            private set;
        }

        public Tile FPawnStartTile
        {
            get;
            private set;
        }

        public Tile GPawnStartTile
        {
            get;
            private set;
        }

        public Tile HPawnStartTile
        {
            get;
            private set;
        }

        public Board()
        {
            Tiles = new Tile[Length, Width];
            Fill();

            QueenRookStartTile = Tiles[7, 0];
            QueenKnightStartTile = Tiles[7, 1];
            QueenBishopStartTile = Tiles[7, 2];
            QueenStartTile = Tiles[7, 3];
            KingStartTile = Tiles[7, 4];
            KingBishopStartTile = Tiles[7, 5];
            KingKnightStartTile = Tiles[7, 6];
            KingRookStartTile = Tiles[7, 7];
            APawnStartTile = Tiles[6, 0];
            BPawnStartTile = Tiles[6, 1];
            CPawnStartTile = Tiles[6, 2];
            DPawnStartTile = Tiles[6, 3];
            EPawnStartTile = Tiles[6, 4];
            FPawnStartTile = Tiles[6, 5];
            GPawnStartTile = Tiles[6, 6];
            HPawnStartTile = Tiles[6, 7];
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
