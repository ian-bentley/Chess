using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player(board);
            board.Draw();

            while (true)
            {
                Console.Write("Enter a coordinate: ");
                string input = Console.ReadLine();
                if (input == "-1")
                {
                    Console.WriteLine("Closing app...");
                    break;
                }
                if (input.Length == 2)
                {
                    char letterCoordinate = input[0];
                    char numberCoordinate = input[1];

                    int coordinateX;
                    int coordinateY;

                    switch (letterCoordinate)
                    {
                        case 'a':
                            coordinateX = 0;
                            break;
                        case 'b':
                            coordinateX = 1;
                            break;
                        case 'c':
                            coordinateX = 2;
                            break;
                        case 'd':
                            coordinateX = 3;
                            break;
                        case 'e':
                            coordinateX = 4;
                            break;
                        case 'f':
                            coordinateX = 5;
                            break;
                        case 'g':
                            coordinateX = 6;
                            break;

                        case 'h':
                            coordinateX = 7;
                            break;
                        default:
                            continue;
                    }

                    switch (numberCoordinate)
                    {
                        case '1':
                            coordinateY = 7;
                            break;
                        case '2':
                            coordinateY = 6;
                            break;
                        case '3':
                            coordinateY = 5;
                            break;
                        case '4':
                            coordinateY = 4;
                            break;
                        case '5':
                            coordinateY = 3;
                            break;
                        case '6':
                            coordinateY = 2;
                            break;
                        case '7':
                            coordinateY = 1;
                            break;

                        case '8':
                            coordinateY = 0;
                            break;
                        default:
                            continue;
                    }

                    if (board.Tiles[coordinateY, coordinateX].OccupyingPiece != null)
                    {
                        Piece selectedPiece = board.Tiles[coordinateY, coordinateX].OccupyingPiece;

                        Console.WriteLine("You select the " + selectedPiece.Name);
                    }
                }
                    
            }
        }
    }
}
