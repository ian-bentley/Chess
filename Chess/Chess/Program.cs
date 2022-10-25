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
                                selectedTile = board.tileAt(ToCoordinate(selectedTileString));

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
                                Tile moveTile = board.tileAt(ToCoordinate(moveString));
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

        static Coordinate ToCoordinate(string input)
        {
            Coordinate coordinate;

            if (input.Length == 2)
            {
                char letterCoordinate = input[0];
                char numberCoordinate = input[1];

                switch (letterCoordinate)
                {
                    case 'a':
                        coordinate.x = 0;
                        break;
                    case 'b':
                        coordinate.x = 1;
                        break;
                    case 'c':
                        coordinate.x = 2;
                        break;
                    case 'd':
                        coordinate.x = 3;
                        break;
                    case 'e':
                        coordinate.x = 4;
                        break;
                    case 'f':
                        coordinate.x = 5;
                        break;
                    case 'g':
                        coordinate.x = 6;
                        break;
                    case 'h':
                        coordinate.x = 7;
                        break;
                    default:
                        throw new InvalidCoordinateInput(input);
                }

                switch (numberCoordinate)
                {
                    case '1':
                        coordinate.y = 7;
                        break;
                    case '2':
                        coordinate.y = 6;
                        break;
                    case '3':
                        coordinate.y = 5;
                        break;
                    case '4':
                        coordinate.y = 4;
                        break;
                    case '5':
                        coordinate.y = 3;
                        break;
                    case '6':
                        coordinate.y = 2;
                        break;
                    case '7':
                        coordinate.y = 1;
                        break;

                    case '8':
                        coordinate.y = 0;
                        break;
                    default:
                        throw new InvalidCoordinateInput(input);
                }

                return coordinate;
            }
            else
            {
                throw new InvalidCoordinateInput(input);
            }
        }

        static bool isValidMove(Tile moveTile, Piece selectedPiece)
        {
            List<Tile> possibleMoveTiles = new List<Tile>();
            List<Tile> validMoveTiles = new List<Tile>();

            switch (selectedPiece.PieceType)
            {
                case PieceType.Pawn:
                    if (!selectedPiece.HasMoved)
                    {
                        // store extra move in possible move tiles
                        try
                        {
                            possibleMoveTiles.Add(board.tileAt(selectedPiece.OccupiedTile.Position.X, selectedPiece.OccupiedTile.Position.Y - 2));
                        }
                        catch (TileOutOfBounds ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    //store possible moves
                    try
                    {
                        possibleMoveTiles.Add(board.tileAt(selectedPiece.OccupiedTile.Position.X, selectedPiece.OccupiedTile.Position.Y - 1));
                    }
                    catch (TileOutOfBounds ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    foreach(Tile t in possibleMoveTiles)
                    {
                        /*if (!t.isOccupied() && board.isPathClear(selectedPiece.OccupiedTile, t))
                        {
                            validMoveTiles.Add(t);
                        }*/

                        if (!t.isOccupied())
                        {
                            validMoveTiles.Add(t);
                        }
                    }

                    if (validMoveTiles.Count == 0)
                    {
                        return false;
                    }

                    foreach (Tile t in validMoveTiles)
                    {
                        if (t == moveTile)
                        {
                            return true;
                        }
                    }

                    // moveTile is not a valid move
                    return false;

                case PieceType.Rook:
                    return false;
                case PieceType.Knight:
                    return false;
                case PieceType.Bishop:
                    return false;
                case PieceType.Queen:
                    return false;
                case PieceType.King:
                    return false;
                default:
                    return false;
            }
        }
    }

    public struct Coordinate
    {
        public int x;
        public int y;
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
    public class TileOutOfBounds : Exception
    { 
        public string Message
        {
            get;
            private set;
        }

        public TileOutOfBounds(int x, int y)
        {
            Message = "Tile at (" + x + ", " + y + ") is out of bounds";
        }
    }
}
