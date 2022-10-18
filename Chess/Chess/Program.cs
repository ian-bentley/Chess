using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Program
    {
        static Board board = new Board();
        static Player player = new Player(board);
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            board.Draw();

            while (true)
            {
                Console.Clear();
                board.Draw();
                string selectedTileString = GetInput("Select a piece: ");
                
                if (selectedTileString == "-1")
                {
                    Console.WriteLine("Closing app...");
                    break;
                }

                try
                {
                    Coordinate coordinate;
                    coordinate = ToCoordinate(selectedTileString);

                    Tile selectedTile = board.tileAt(ToCoordinate(selectedTileString));

                    if (selectedTile.OccupyingPiece != null)
                    {
                        Piece selectedPiece = selectedTile.OccupyingPiece;
                        Console.WriteLine("You selected the " + selectedPiece.Name);

                        string moveString = GetInput("Enter your move: ");

                        selectedPiece.MoveTo(selectedTile);

                        Tile moveTile = board.tileAt(ToCoordinate(moveString));
                        selectedTile.OccupyingPiece = null;
                        selectedPiece.MoveTo(moveTile);
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
    }

    struct Coordinate
    {
        public int x;
        public int y;
    }

    [Serializable]
    class InvalidCoordinateInput : Exception
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

    [Serializable]
    class NoPieceOnTile : Exception
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
