using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King, Null };
    public class Piece
    {
        public PieceType PieceType
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string TileIcon
        {
            get;
            private set;
        }

        public bool HasMoved
        {
            get;
            set;
        }

        public Tile OccupiedTile
        {
            get;
            set;
        }

        public Piece() : this(PieceType.Null, "") { }

        public Piece(PieceType pieceType, string name)
        {
            PieceType = pieceType;
            Name = name;

            switch(PieceType)
            {
                case PieceType.Pawn:
                    TileIcon = "P";
                    break;
                case PieceType.Rook:
                    TileIcon = "R";
                    break;
                case PieceType.Knight:
                    TileIcon = "N";
                    break;
                case PieceType.Bishop:
                    TileIcon = "B";
                    break;
                case PieceType.Queen:
                    TileIcon = "Q";
                    break;
                case PieceType.King:
                    TileIcon = "K";
                    break;
                default:
                    TileIcon = " ";
                    break;
            }

            HasMoved = false;
        }

        public void MoveTo(Tile tile)
        {
            if (!HasMoved)
            {
                HasMoved = true;
            }
            tile.OccupyingPiece = this;
            OccupiedTile.OccupyingPiece = null;
            OccupiedTile = tile;
        }

        public void Set(Tile tile)
        {
            tile.OccupyingPiece = this;
            OccupiedTile = tile;
        }
    }
}
