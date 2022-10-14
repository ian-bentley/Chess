using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Tile
    {
        const string tileEmptyIcon = "[ ]";
        public Position Position
        {
            get;
            private set;
        }
        
        public Piece OccupyingPiece
        {
            get;
            set;
        }

        public Tile()
        {
            Position = new Position();
        }
        
        public string getTileIcon()
        {
            string tileIcon;
            if (OccupyingPiece == null)
            {
                tileIcon = tileEmptyIcon;
            }
            else
            {
                tileIcon = OccupyingPiece.TileIcon;
            }
            return tileIcon;
        }
    }
}
