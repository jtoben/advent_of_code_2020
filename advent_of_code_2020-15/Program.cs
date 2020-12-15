using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_15
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();

            Console.WriteLine(PartOne(numbers));
            Console.WriteLine(PartTwo(numbers));
        }

        static int PartOne(int[] numbers) {
            return CalculateLastNumber(numbers, 2020);
        }

        static int PartTwo(int[] numbers) {
            return CalculateLastNumber(numbers, 30_000_000);
        }

        static int CalculateLastNumber(int[] numbers, int maximumCount) {
            int lastNumber = numbers[numbers.Length - 1];

            Dictionary<int, int> spokenNumbers = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Length - 1; i++) {
                spokenNumbers[numbers[i]] = i + 1;
            }

            for (int i = numbers.Length; i < maximumCount; i++) {
                if (spokenNumbers.ContainsKey(lastNumber)) {

                    var turn = spokenNumbers[lastNumber];
                    spokenNumbers[lastNumber] = i;
                    lastNumber = i - turn;

                } else {
                    spokenNumbers[lastNumber] = i;
                    lastNumber = 0;
                }
            }

            return lastNumber;
        }
    }
}
