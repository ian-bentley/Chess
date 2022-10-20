using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Player
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

        public Board Board
        {
            get;
            private set;
        }

        public Player(Board board)
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

            Board = board;

            SetUpPieces();
        }

        public void SetUpPieces()
        {
            QueenRook.Set(Board.QueenRookStartTile);
            QueenKnight.Set(Board.QueenKnightStartTile);
            QueenBishop.Set(Board.QueenBishopStartTile);
            Queen.Set(Board.QueenStartTile);
            King.Set(Board.KingStartTile);
            KingBishop.Set(Board.KingBishopStartTile);
            KingKnight.Set(Board.KingKnightStartTile);
            KingRook.Set(Board.KingRookStartTile);
            APawn.Set(Board.APawnStartTile);
            BPawn.Set(Board.BPawnStartTile);
            CPawn.Set(Board.CPawnStartTile);
            DPawn.Set(Board.DPawnStartTile);
            EPawn.Set(Board.EPawnStartTile);
            FPawn.Set(Board.FPawnStartTile);
            GPawn.Set(Board.GPawnStartTile);
            HPawn.Set(Board.HPawnStartTile);
        }
    }
}
