using System;
using System.IO;

namespace advent_of_code_2020_3
{
    class Program
    {
        private const char tree = '#';

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            int numberOfTrees = PartOne(lines);
            Console.WriteLine(numberOfTrees);

            long numberOfTreesMultiplied = PartTwo(lines);
            Console.WriteLine(numberOfTreesMultiplied);
        }

        static int PartOne(string[] lines) {
            return TraverseDownHill(lines, 3, 1);
        }

        static long PartTwo(string[] lines) {

            long numberOfTreesMultiplied = 1;
            for (int i = 0; i < 5; i++) {
                int xTraversal = (1 + 2 * i) % 8;
                int yTraversal = (1 + i / 4);

                int numberOfTrees = TraverseDownHill(lines, xTraversal, yTraversal);
                numberOfTreesMultiplied *= numberOfTrees;
            }

            return numberOfTreesMultiplied;
        }

        static int TraverseDownHill(string[] lines, int xTraversal, int yTraversal) {
            int x = xTraversal;
            int y = yTraversal;

            int count = 0;

            while (y < lines.Length) {
                if (lines[y][x] == tree) {
                    count++;
                }

                x = (x + xTraversal) % lines[0].Length;
                y += yTraversal;
            }

            return count;
        }
    }
}
