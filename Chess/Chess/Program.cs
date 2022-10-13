using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            string input = Console.ReadLine();
            Console.WriteLine(input);*/

            string emptyTile = "[ ]";
            int boardLength = 8;
            int boardWidth = 8;
            for (int i = 0; i < boardLength; i ++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    Console.Write(emptyTile);
                }
                Console.Write("\n");
            }
        }
    }
}
