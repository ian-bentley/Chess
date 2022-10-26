using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    enum GameState { Start, PlayerTurn, Close }
    enum TurnState { SelectingPiece, SelectingMove, TurnOver }
    public class Program
    {
        static Board board = new Board();
        static Player player = new Player(board);
        static GameState gameState = GameState.Start;
        static string exitCommand = "-99";
        static string backCommand = "-1";
        static void Main(string[] args)
        {
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
                        gameState = GameState.PlayerTurn;
                        break;
                    case GameState.PlayerTurn:
                        TurnState turnState = TurnState.SelectingPiece;
                        Tile selectedTile = new Tile();
                        Piece selectedPiece = new Piece();

                        while (turnState == TurnState.SelectingPiece)
                        {
                            Console.Clear();
                            board.Draw();
                            string selectedTileString = GetInput("Select a piece: ");

                            if (selectedTileString == exitCommand)
                            {
                                turnState = TurnState.TurnOver;
                                gameState = GameState.Close;
                                continue;
                            }

                            try
                            {
                                selectedTile = board.tileAt(ToPosition(selectedTileString));

                                if (selectedTile.OccupyingPiece != null)
                                {
                                    turnState = TurnState.SelectingMove;
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
                        }

                        while (turnState == TurnState.SelectingMove)
                        {
                            selectedPiece = selectedTile.OccupyingPiece;

                            Console.Clear();
                            board.Draw();
                            Console.WriteLine("You selected the " + selectedPiece.Name);
                            string moveString = GetInput("Enter your move: ");

                            if (moveString == backCommand)
                            {
                                turnState = TurnState.SelectingPiece;
                                continue;
                            }

                            try
                            {
                                Tile moveTile = board.tileAt(ToPosition(moveString));
                                
                                if (isValidMove(moveTile, selectedPiece))
                                {
                                    selectedTile.OccupyingPiece = null;
                                    selectedPiece.MoveTo(moveTile);
                                    turnState = TurnState.TurnOver;
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

        static bool isValidMove(Tile moveTile, Piece selectedPiece)
        {
            List<Tile> validMoveTiles = new List<Tile>();

            switch (selectedPiece.PieceType)
            {
                case PieceType.Pawn:
                    //search for north moves
                    for (int i = 1; selectedPiece.HasMoved && i < 2 || !selectedPiece.HasMoved && i < 3; i++)
                    {
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                case PieceType.Rook:
                    //search for west moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);
                        
                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }
                        
                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                case PieceType.Knight:
                    Position[] possibleMovePositionsKnight = 
                    { 
                        new Position(selectedPiece.OccupiedTile.Position.X - 2, selectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(selectedPiece.OccupiedTile.Position.X - 1, selectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(selectedPiece.OccupiedTile.Position.X + 1, selectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(selectedPiece.OccupiedTile.Position.X + 2, selectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(selectedPiece.OccupiedTile.Position.X + 2, selectedPiece.OccupiedTile.Position.Y + 1),
                        new Position(selectedPiece.OccupiedTile.Position.X + 1, selectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(selectedPiece.OccupiedTile.Position.X - 1, selectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(selectedPiece.OccupiedTile.Position.X - 2, selectedPiece.OccupiedTile.Position.Y + 1),
                    };

                    foreach (Position p in possibleMovePositionsKnight)
                    {
                        if (board.isInBounds(p.X, p.Y))
                        {
                            Tile possibleMoveTile = board.tileAt(p.X, p.Y);
                            if (possibleMoveTile.isOccupied())
                            {
                                if (false) // if piece is enemy piece
                                {
                                    validMoveTiles.Add(possibleMoveTile);
                                }
                                break;
                            }

                            validMoveTiles.Add(possibleMoveTile);
                        }
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                case PieceType.Bishop:
                    // search for northwest moves
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                case PieceType.Queen:
                    for (int i = 1; i < board.Length; i++)
                    {
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y - i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X + i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
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
                        int possibleMoveTileX = selectedPiece.OccupiedTile.Position.X - i;
                        int possibleMoveTileY = selectedPiece.OccupiedTile.Position.Y + i;
                        if (!board.isInBounds(possibleMoveTileX, possibleMoveTileY))
                        {
                            break;
                        }

                        Tile possibleMoveTile = board.tileAt(possibleMoveTileX, possibleMoveTileY);

                        if (possibleMoveTile.isOccupied())
                        {
                            if (false) // if piece is enemy piece
                            {
                                validMoveTiles.Add(possibleMoveTile);
                            }
                            break;
                        }

                        validMoveTiles.Add(possibleMoveTile);
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                case PieceType.King:
                    Position[] possibleMovePositionsKing =
                    {
                        new Position(selectedPiece.OccupiedTile.Position.X - 2, selectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(selectedPiece.OccupiedTile.Position.X - 1, selectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(selectedPiece.OccupiedTile.Position.X + 1, selectedPiece.OccupiedTile.Position.Y - 2),
                        new Position(selectedPiece.OccupiedTile.Position.X + 2, selectedPiece.OccupiedTile.Position.Y - 1),
                        new Position(selectedPiece.OccupiedTile.Position.X + 2, selectedPiece.OccupiedTile.Position.Y + 1),
                        new Position(selectedPiece.OccupiedTile.Position.X + 1, selectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(selectedPiece.OccupiedTile.Position.X - 1, selectedPiece.OccupiedTile.Position.Y + 2),
                        new Position(selectedPiece.OccupiedTile.Position.X - 2, selectedPiece.OccupiedTile.Position.Y + 1),
                    };

                    foreach (Position p in possibleMovePositionsKing)
                    {
                        if (board.isInBounds(p.X, p.Y))
                        {
                            Tile possibleMoveTile = board.tileAt(p.X, p.Y);
                            if (possibleMoveTile.isOccupied())
                            {
                                if (false) // if piece is enemy piece
                                {
                                    validMoveTiles.Add(possibleMoveTile);
                                }
                                break;
                            }

                            validMoveTiles.Add(possibleMoveTile);
                        }
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    return false;
                default:
                    return false;
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
}
