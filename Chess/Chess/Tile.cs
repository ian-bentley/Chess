using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Tile
    {
        Piece occupyingPiece;
        string tileEmptyIcon = "[ ]";
        public string getTileIcon()
        {
            string tileIcon;
            if (occupyingPiece == null)
            {
                tileIcon = tileEmptyIcon;
            }
            else
            {
                tileIcon = occupyingPiece.TileIcon;
            }
            return tileIcon;
        }
    }
}
