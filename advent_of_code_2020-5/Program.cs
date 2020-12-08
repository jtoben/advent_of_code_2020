using System;
using System.IO;
using System.Collections.Generic;

namespace advent_of_code_2020_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            Console.WriteLine(PartOne(lines));
            Console.WriteLine(PartTwo(lines));
        }

        static int PartOne(string[] lines) {

            var seatIdentifiers = GetSeatIdentifiers(lines);

            return seatIdentifiers[seatIdentifiers.Count - 1];
        }

        static int PartTwo(string[] lines) {

            var seatIdentifiers = GetSeatIdentifiers(lines);

            var previousIdentifier = seatIdentifiers[0] - 1;
            int missingIdentifier = 0;
            foreach (var seatId in seatIdentifiers) {
                if (seatId - previousIdentifier > 1) {
                    missingIdentifier = previousIdentifier + 1;
                    break;
                }

                previousIdentifier = seatId;
            }

            return missingIdentifier;
        }

        static List<int> GetSeatIdentifiers(string[] lines) {
            List<int> seatIdentifiers = new List<int>();
            foreach (var line in lines) {

                var frontBackSpecification = line.Substring(0, 7);
                var leftRightSpecification = line.Substring(7, 3);

                int row = BinarySpaceToNumber(frontBackSpecification, 127);
                int column = BinarySpaceToNumber(leftRightSpecification, 7);
                var seatId = row * 8 + column;

                seatIdentifiers.Add(seatId);

            }

            seatIdentifiers.Sort();

            return seatIdentifiers;
        }

        static int BinarySpaceToNumber(string specification, int max) {
            int min = 0;
            foreach (var c in specification) {
                bool upper = (c == 'B' || c == 'R');

                int halfDifference = (max - min) / 2 + 1;

                if (upper) {
                    min += halfDifference;
                } else {
                    max -= halfDifference;
                }
            }

            return max;
        }
    }
}
