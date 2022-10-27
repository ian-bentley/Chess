using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public enum GameState { Start, WhiteTurn, BlackTurn, Close }
    public static class Program
    {
        static Board board;
        static Player whitePlayer;
        static public Player BlackPlayer
        {
            get;
            private set;
        }
        static Player currentPlayer;
        static GameState gameState;
        const string exitCommand = "-99";
        const string backCommand = "-1";
        static void Main(string[] args)
        {
            board = new Board();
            whitePlayer = new Player();
            BlackPlayer = new Player();
            gameState = GameState.Start;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            board.Draw();

            while (gameState != GameState.Close)
            {
                Console.Clear();
                board.Draw();
                
                switch (gameState)
                {
                    case GameState.Start:
                        whitePlayer.QueenRookStartTile = board.Tiles[7, 0];
                        whitePlayer.QueenKnightStartTile = board.Tiles[7, 1];
                        whitePlayer.QueenBishopStartTile = board.Tiles[7, 2];
                        whitePlayer.QueenStartTile = board.Tiles[7, 3];
                        whitePlayer.KingStartTile = board.Tiles[7, 4];
                        whitePlayer.KingBishopStartTile = board.Tiles[7, 5];
                        whitePlayer.KingKnightStartTile = board.Tiles[7, 6];
                        whitePlayer.KingRookStartTile = board.Tiles[7, 7];
                        whitePlayer.APawnStartTile = board.Tiles[6, 0];
                        whitePlayer.BPawnStartTile = board.Tiles[6, 1];
                        whitePlayer.CPawnStartTile = board.Tiles[6, 2];
                        whitePlayer.DPawnStartTile = board.Tiles[6, 3];
                        whitePlayer.EPawnStartTile = board.Tiles[6, 4];
                        whitePlayer.FPawnStartTile = board.Tiles[6, 5];
                        whitePlayer.GPawnStartTile = board.Tiles[6, 6];
                        whitePlayer.HPawnStartTile = board.Tiles[6, 7];

                        BlackPlayer.QueenRookStartTile = board.Tiles[0, 0];
                        BlackPlayer.QueenKnightStartTile = board.Tiles[0, 1];
                        BlackPlayer.QueenBishopStartTile = board.Tiles[0, 2];
                        BlackPlayer.QueenStartTile = board.Tiles[0, 3];
                        BlackPlayer.KingStartTile = board.Tiles[0, 4];
                        BlackPlayer.KingBishopStartTile = board.Tiles[0, 5];
                        BlackPlayer.KingKnightStartTile = board.Tiles[0, 6];
                        BlackPlayer.KingRookStartTile = board.Tiles[0, 7];
                        BlackPlayer.APawnStartTile = board.Tiles[1, 0];
                        BlackPlayer.BPawnStartTile = board.Tiles[1, 1];
                        BlackPlayer.CPawnStartTile = board.Tiles[1, 2];
                        BlackPlayer.DPawnStartTile = board.Tiles[1, 3];
                        BlackPlayer.EPawnStartTile = board.Tiles[1, 4];
                        BlackPlayer.FPawnStartTile = board.Tiles[1, 5];
                        BlackPlayer.GPawnStartTile = board.Tiles[1, 6];
                        BlackPlayer.HPawnStartTile = board.Tiles[1, 7];
                        SetUpPieces();
                        gameState = GameState.WhiteTurn;
                        break;
                    case GameState.WhiteTurn:
                        currentPlayer = whitePlayer;
                        whitePlayer.TurnState = TurnState.SelectingPiece;
                        while (whitePlayer.TurnState == TurnState.SelectingPiece)
                        {
                            SelectPiece();
                        }

                        while (whitePlayer.TurnState == TurnState.SelectingMove)
                        {
                            SelectMove();
                        }

                        if (gameState != GameState.Close)
                        {
                            gameState = GameState.BlackTurn;
                        }
                        break;
                    case GameState.BlackTurn:
                        currentPlayer = BlackPlayer;
                        BlackPlayer.TurnState = TurnState.SelectingPiece;
                        while (BlackPlayer.TurnState == TurnState.SelectingPiece)
                        {
                            SelectPiece();
                        }

                        while (BlackPlayer.TurnState == TurnState.SelectingMove)
                        {
                            SelectMove();
                        }

                        if (gameState != GameState.Close)
                        {
                            gameState = GameState.WhiteTurn;
                        }
                        break;
                }
            }

            // Close app
            Console.WriteLine("Closing app...");
        }

        static string GetInput (string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            return input;
        }

        static Position ToPosition(string input)
        {
            Position position = new Position();

            if (input.Length == 2)
            {
                char letterCoordinate = input[0];
                char numberCoordinate = input[1];

                switch (letterCoordinate)
                {
                    case 'a':
                        position.X = 0;
                        break;
                    case 'b':
                        position.X = 1;
                        break;
                    case 'c':
                        position.X = 2;
                        break;
                    case 'd':
                        position.X = 3;
                        break;
                    case 'e':
                        position.X = 4;
                        break;
                    case 'f':
                        position.X = 5;
                        break;
                    case 'g':
                        position.X = 6;
                        break;
                    case 'h':
                        position.X = 7;
                        break;
                    default:
                        throw new InvalidCoordinateInput(input);
                }

                switch (numberCoordinate)
                {
                    case '1':
                        position.Y = 7;
                        break;
                    case '2':
                        position.Y = 6;
                        break;
                    case '3':
                        position.Y = 5;
                        break;
                    case '4':
                        position.Y = 4;
                        break;
                    case '5':
                        position.Y = 3;
                        break;
                    case '6':
                        position.Y = 2;
                        break;
                    case '7':
                        position.Y = 1;
                        break;

                    case '8':
                        position.Y = 0;
                        break;
                    default:
                        throw new InvalidCoordinateInput(input);
                }

                return position;
            }
            else
            {
                throw new InvalidCoordinateInput(input);
            }
        }

        static bool isValidMove(Tile moveTile)
        {
            List<Tile> validMoveTiles = new List<Tile>();

            switch (currentPlayer.SelectedPiece.PieceType)
            {
                case PieceType.Pawn:
                    if (currentPlayer == whitePlayer)
                    {
                        List<Position> possibleMovePositionsPawn = new List<Position>();
                        if (!board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1).isOccupied())
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                        }

                        if (!currentPlayer.SelectedPiece.HasMoved && !board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 2).isOccupied())
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 2));
                        }

                        if (board.isInBounds(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1) 
                            && board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1).isOccupied() 
                            && !currentPlayer.HasPiece(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1).OccupyingPiece))
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                        }

                        if (board.isInBounds(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1)
                            && board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1).isOccupied()
                            && !currentPlayer.HasPiece(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1).OccupyingPiece))
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                        }
                    }
                    else if (currentPlayer == BlackPlayer)
                    {
                        List<Position> possibleMovePositionsPawn = new List<Position>();
                        if (!board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1).isOccupied())
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));
                        }

                        if (!currentPlayer.SelectedPiece.HasMoved && !board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 2).isOccupied())
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 2));
                        }

                        if (board.isInBounds(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1)
                            && board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1).isOccupied()
                            && !currentPlayer.HasPiece(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1).OccupyingPiece))
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));
                        }

                        if (board.isInBounds(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1)
                            && board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1).isOccupied()
                            && !currentPlayer.HasPiece(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1).OccupyingPiece))
                        {
                            validMoveTiles.Add(board.tileAt(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));
                        }
                    }

                    break;
                case PieceType.Rook:
                    //search for west moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);
                        
                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }
                        
                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for north moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for east moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for south moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    break;
                case PieceType.Knight:
                    Position[] possibleMovePositionsKnight = 
                    { 
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 2, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 2, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 2, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 2, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1),
                    };

                    foreach (Position p in possibleMovePositionsKnight)
                    {
                        if (board.isInBounds(p.X, p.Y))
                        {
                            Tile possibleMoveTile = board.tileAt(p.X, p.Y);
                            if (possibleMoveTile.isOccupied())
                            {
                                if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                                {
                                    validMoveTiles.Add(possibleMoveTile);
                                }
                            }

                            validMoveTiles.Add(possibleMoveTile);
                        }
                    }

                    break;
                case PieceType.Bishop:
                    // search for northwest moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for northeast moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for southeast moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for southwest moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    break;
                case PieceType.Queen:
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for north moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for east moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for south moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    // search for northwest moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for northeast moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for southeast moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    //search for southwest moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = currentPlayer.SelectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = currentPlayer.SelectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    break;
                case PieceType.King:
                    List<Position> possibleMovePositionsKing = new List<Position>();
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y - 1));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X + 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));
                    possibleMovePositionsKing.Add(new Position(currentPlayer.SelectedPiece.OccupiedTile.Position.X - 1, currentPlayer.SelectedPiece.OccupiedTile.Position.Y + 1));

                    if (!currentPlayer.SelectedPiece.HasMoved)
                    {
                        if (!currentPlayer.KingBishopStartTile.isOccupied() && !currentPlayer.KingKnightStartTile.isOccupied() && !currentPlayer.KingRook.HasMoved) //kingside castling
                        {
                            validMoveTiles.Add(currentPlayer.KingKnightStartTile);
                        }

                        if (!currentPlayer.QueenBishopStartTile.isOccupied() && !currentPlayer.QueenKnightStartTile.isOccupied() && !currentPlayer.QueenStartTile.isOccupied() && !currentPlayer.QueenRook.HasMoved) //queenside castling
                        {
                            validMoveTiles.Add(currentPlayer.QueenBishopStartTile);
                        }
                    }

                    foreach (Position p in possibleMovePositionsKing)
                    {
                        if (board.isInBounds(p.X, p.Y))
                        {
                            Tile possibleMoveTile = board.tileAt(p.X, p.Y);
                            if (possibleMoveTile.isOccupied())
                            {
                                if (!currentPlayer.HasPiece(possibleMoveTile.OccupyingPiece)) // if piece is enemy piece
                                {
                                    validMoveTiles.Add(possibleMoveTile);
                                }
                            }

                            validMoveTiles.Add(possibleMoveTile);
                        }
                    }
                    break;
                default:
                    break;
            }

            foreach (Tile t in validMoveTiles)
            {
                if (t == moveTile)
                {
                    return true;
                }
            }

            return false;
        }

        public static void SetUpPieces()
        {
            whitePlayer.QueenRook.Set(whitePlayer.QueenRookStartTile);
            whitePlayer.QueenKnight.Set(whitePlayer.QueenKnightStartTile);
            whitePlayer.QueenBishop.Set(whitePlayer.QueenBishopStartTile);
            whitePlayer.Queen.Set(whitePlayer.QueenStartTile);
            whitePlayer.King.Set(whitePlayer.KingStartTile);
            whitePlayer.KingBishop.Set(whitePlayer.KingBishopStartTile);
            whitePlayer.KingKnight.Set(whitePlayer.KingKnightStartTile);
            whitePlayer.KingRook.Set(whitePlayer.KingRookStartTile);
            whitePlayer.APawn.Set(whitePlayer.APawnStartTile);
            whitePlayer.BPawn.Set(whitePlayer.BPawnStartTile);
            whitePlayer.CPawn.Set(whitePlayer.CPawnStartTile);
            whitePlayer.DPawn.Set(whitePlayer.DPawnStartTile);
            whitePlayer.EPawn.Set(whitePlayer.EPawnStartTile);
            whitePlayer.FPawn.Set(whitePlayer.FPawnStartTile);
            whitePlayer.GPawn.Set(whitePlayer.GPawnStartTile);
            whitePlayer.HPawn.Set(whitePlayer.HPawnStartTile);

            BlackPlayer.QueenRook.Set(BlackPlayer.QueenRookStartTile);
            BlackPlayer.QueenKnight.Set(BlackPlayer.QueenKnightStartTile);
            BlackPlayer.QueenBishop.Set(BlackPlayer.QueenBishopStartTile);
            BlackPlayer.Queen.Set(BlackPlayer.QueenStartTile);
            BlackPlayer.King.Set(BlackPlayer.KingStartTile);
            BlackPlayer.KingBishop.Set(BlackPlayer.KingBishopStartTile);
            BlackPlayer.KingKnight.Set(BlackPlayer.KingKnightStartTile);
            BlackPlayer.KingRook.Set(BlackPlayer.KingRookStartTile);
            BlackPlayer.APawn.Set(BlackPlayer.APawnStartTile);
            BlackPlayer.BPawn.Set(BlackPlayer.BPawnStartTile);
            BlackPlayer.CPawn.Set(BlackPlayer.CPawnStartTile);
            BlackPlayer.DPawn.Set(BlackPlayer.DPawnStartTile);
            BlackPlayer.EPawn.Set(BlackPlayer.EPawnStartTile);
            BlackPlayer.FPawn.Set(BlackPlayer.FPawnStartTile);
            BlackPlayer.GPawn.Set(BlackPlayer.GPawnStartTile);
            BlackPlayer.HPawn.Set(BlackPlayer.HPawnStartTile);
        }

        public static void SelectPiece()
        {
            Console.Clear();
            board.Draw();
            string selectedTileString = GetInput("Select a piece: ");

            if (selectedTileString == exitCommand)
            {
                currentPlayer.TurnState = TurnState.TurnOver;
                gameState = GameState.Close;
                return;
            }

            try
            {
                currentPlayer.SelectedTile = board.tileAt(ToPosition(selectedTileString));

                if (currentPlayer.SelectedTile.OccupyingPiece != null)
                {
                    currentPlayer.SelectedPiece = currentPlayer.SelectedTile.OccupyingPiece;
                    if (currentPlayer.HasPiece(currentPlayer.SelectedPiece))
                    {
                        currentPlayer.TurnState = TurnState.SelectingMove;
                    }
                    else
                    {
                        throw new InvalidPiece(selectedTileString);
                    }
                }
                else
                {
                    throw new NoPieceOnTile(selectedTileString);
                }
            }
            catch (InvalidCoordinateInput ex)
            {
                Console.WriteLine("\"" + ex.InputString + "\" is not a valid input");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            catch (NoPieceOnTile ex)
            {
                Console.WriteLine("No piece is on " + "\"" + ex.InputString + "\"");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            catch (InvalidPiece ex)
            {
                Console.WriteLine("\"" + ex.InputString + "\"" + " is not your piece");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public static void SelectMove()
        {
            Console.Clear();
            board.Draw();
            Console.WriteLine("You selected the " + currentPlayer.SelectedPiece.Name);
            string moveString = GetInput("Enter your move: ");

            if (moveString == backCommand)
            {
                currentPlayer.TurnState = TurnState.SelectingPiece;
                return;
            }

            try
            {
                Tile moveTile = board.tileAt(ToPosition(moveString));

                if (isValidMove(moveTile))
                {
                    if (currentPlayer.SelectedPiece.PieceType == PieceType.King && !currentPlayer.SelectedPiece.HasMoved)
                    {
                        if (moveTile == currentPlayer.KingKnightStartTile)
                        {
                            currentPlayer.KingRook.MoveTo(currentPlayer.KingBishopStartTile);
                        }
                        else if (moveTile == currentPlayer.QueenBishopStartTile)
                        {
                            currentPlayer.KingRook.MoveTo(currentPlayer.QueenStartTile);
                        }
                    }
                    currentPlayer.SelectedPiece.MoveTo(moveTile);
                    currentPlayer.TurnState = TurnState.TurnOver;
                }
                else
                {
                    throw new InvalidMove(moveString);
                }
            }
            catch (InvalidCoordinateInput ex)
            {
                Console.WriteLine("\"" + ex.InputString + "\" is not a valid input");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            catch (InvalidMove ex)
            {
                Console.WriteLine("\"" + ex.InputString + "\" is not a valid move");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    [Serializable]
    public class InvalidCoordinateInput : Exception
    {
        public string InputString
        {
            get;
            private set;
        }
        public InvalidCoordinateInput(string input)
        {
            InputString = input;
        }
    }

    public class InvalidMove : Exception
    {
        public string InputString
        {
            get;
            private set;
        }
        public InvalidMove(string input)
        {
            InputString = input;
        }
    }

    [Serializable]
    public class NoPieceOnTile : Exception
    {
        public string InputString
        {
            get;
            private set;
        }

        public NoPieceOnTile(string input)
        {
            InputString = input;
        }
    }

    [Serializable]
    public class InvalidPiece : Exception
    {
        public string InputString
        {
            get;
            private set;
        }

        public InvalidPiece(string input)
        {
            InputString = input;
        }
    }
}
