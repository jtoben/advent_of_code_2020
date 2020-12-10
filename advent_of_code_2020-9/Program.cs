using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2020_9
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] numbers = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            Console.WriteLine(PartOne(numbers));
            Console.WriteLine(PartTwo(numbers));
        }

        static long PartOne(long[] numbers) {
            int pointer = 25;

            while (true) {
                long number = numbers[pointer];

                bool success = false;
                for (int i = pointer - 25; i < pointer; i++) {
                    for (int j = i + 1; j < pointer; j++) {
                        if (numbers[i] + numbers[j] == number) {
                            success = true;
                            break;
                        }
                    }
                    
                    if (success) {
                        break;
                    }
                }

                if (!success) {
                    return numbers[pointer];
                }

                pointer++;
            }
        }

        static long PartTwo(long[] numbers) {
            long invalidNumber = PartOne(numbers);
            int index = Array.FindIndex(numbers, x => x == invalidNumber);

            for (int i = 0; i < index; i++) {
                for (int j = i + 1; j < index; j++) {
                    var range = numbers[i .. j];
                    long sum = range.Sum();

                    if (sum == invalidNumber) {
                        return range.Min() + range.Max();
                    } else if (sum > invalidNumber) {
                        break;
                    }
                }
            }

            return -1L;
        }
    }
}