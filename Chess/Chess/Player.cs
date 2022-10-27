using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public enum TurnState { SelectingPiece, SelectingMove, TurnOver }

    public class Player
    {
        public Piece QueenRook
        {
            get;
            private set;
        }
        public Piece KingRook
        {
            get;
            private set;
        }
        public Piece QueenBishop
        {
            get;
            private set;
        }
        public Piece KingBishop
        {
            get;
            private set;
        }
        public Piece QueenKnight
        {
            get;
            private set;
        }
        public Piece KingKnight
        {
            get;
            private set;
        }
        public Piece Queen
        {
            get;
            private set;
        }
        public Piece King
        {
            get;
            private set;
        }
        public Piece APawn
        {
            get;
            private set;
        }
        public Piece BPawn
        {
            get;
            private set;
        }
        public Piece CPawn
        {
            get;
            private set;
        }
        public Piece DPawn
        {
            get;
            private set;
        }
        public Piece EPawn
        {
            get;
            private set;
        }
        public Piece FPawn
        {
            get;
            private set;
        }
        public Piece GPawn
        {
            get;
            private set;
        }
        public Piece HPawn
        {
            get;
            private set;
        }

        public Tile QueenRookStartTile
        {
            get;
            set;
        }

        public Tile QueenKnightStartTile
        {
            get;
            set;
        }

        public Tile QueenBishopStartTile
        {
            get;
            set;
        }

        public Tile QueenStartTile
        {
            get;
            set;
        }

        public Tile KingStartTile
        {
            get;
            set;
        }

        public Tile KingBishopStartTile
        {
            get;
            set;
        }

        public Tile KingKnightStartTile
        {
            get;
            set;
        }

        public Tile KingRookStartTile
        {
            get;
            set;
        }

        public Tile APawnStartTile
        {
            get;
            set;
        }

        public Tile BPawnStartTile
        {
            get;
            set;
        }

        public Tile CPawnStartTile
        {
            get;
            set;
        }

        public Tile DPawnStartTile
        {
            get;
            set;
        }

        public Tile EPawnStartTile
        {
            get;
            set;
        }

        public Tile FPawnStartTile
        {
            get;
            set;
        }

        public Tile GPawnStartTile
        {
            get;
            set;
        }

        public Tile HPawnStartTile
        {
            get;
            set;
        }

        public TurnState TurnState
        {
            get;
            set;
        }

        public Tile SelectedTile
        {
            get;
            set;
        }

        public Piece SelectedPiece
        {
            get;
            set;
        }

        public Player()
        {
            QueenRook = new Piece(PieceType.Rook, "Queen Rook");
            KingRook = new Piece(PieceType.Rook, "King Rook");
            QueenKnight = new Piece(PieceType.Knight, "Queen Knight");
            KingKnight = new Piece(PieceType.Knight, "King Knight");
            QueenBishop = new Piece(PieceType.Bishop, "Queen Bishop");
            KingBishop = new Piece(PieceType.Bishop, "King Bishop");
            Queen = new Piece(PieceType.Queen, "Queen");
            King = new Piece(PieceType.King, "King");
            APawn = new Piece(PieceType.Pawn, "A-Pawn");
            BPawn = new Piece(PieceType.Pawn, "B-Pawn");
            CPawn = new Piece(PieceType.Pawn, "C-Pawn");
            DPawn = new Piece(PieceType.Pawn, "D-Pawn");
            EPawn = new Piece(PieceType.Pawn, "E-Pawn");
            FPawn = new Piece(PieceType.Pawn, "F-Pawn");
            GPawn = new Piece(PieceType.Pawn, "G-Pawn");
            HPawn = new Piece(PieceType.Pawn, "H-Pawn");
        }

        public bool HasPiece(Piece piece)
        {
            if (piece == QueenRook)
            {
                return true;
            }
            else if (piece == QueenKnight)
            {
                return true;
            }
            else if(piece == QueenBishop)
            {
                return true;
            }
            else if(piece == Queen)
            {
                return true;
            }
            else if(piece == King)
            {
                return true;
            }
            else if(piece == KingBishop)
            {
                return true;
            }
            else if(piece == KingKnight)
            {
                return true;
            }
            else if(piece == KingRook)
            {
                return true;
            }
            else if(piece == APawn)
            {
                return true;
            }
            else if(piece == BPawn)
            {
                return true;
            }
            else if(piece == CPawn)
            {
                return true;
            }
            else if(piece == DPawn)
            {
                return true;
            }
            else if(piece == EPawn)
            {
                return true;
            }
            else if(piece == FPawn)
            {
                return true;
            }
            else if(piece == GPawn)
            {
                return true;
            }
            else if(piece == HPawn)
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
