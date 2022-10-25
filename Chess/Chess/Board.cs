using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Board
    {
        const int length = 8;
        const int width = 8;

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
            Tiles = new Tile[length, width];
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
            for (int i = 0; i < length; i++)
            {
                int rowNumber = 8 - i;
                Console.Write(rowNumber + " ");

                for (int j = 0; j < width; j++)
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

        public Tile tileAt(Coordinate coordinate)
        {
            Tile tile;
            tile = Tiles[coordinate.y, coordinate.x];
            return tile;
        }

        public Tile tileAt(int x, int y)
        {
            Tile tile;
            if (x < 0 || x > width || y < 0 || y > length)
            {
                throw new TileOutOfBounds(x, y);
            }
            tile = Tiles[y, x];
            return tile;
        }

        /*public bool isPathClear(Tile startTile, Tile endTile)
        {
            
        }*/
    }
}
