using System;
using System.IO;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            PartOne(lines);
            PartTwo(lines);
        }

        static void PartOne(string[] lines) {
            for (int i = 0; i < lines.Length; i++) {
                for (int j = i + 1; j < lines.Length; j++) {
                    int firstExpense = int.Parse(lines[i]);
                    int secondExpense = int.Parse(lines[j]);

                    int sum = firstExpense + secondExpense;

                    if (sum == 2020) {
                        Console.WriteLine($"{firstExpense * secondExpense}");
                    }
                }
            }
        }

        static void PartTwo(string[] lines) {
            for (int i = 0; i < lines.Length; i++) {
                for (int j = i + 1; j < lines.Length; j++) {
                    for (int k = j + 1; k < lines.Length; k++) {
                    
                        int firstExpense = int.Parse(lines[i]);
                        int secondExpense = int.Parse(lines[j]);
                        int thirdExpense = int.Parse(lines[k]);

                        int sum = firstExpense + secondExpense + thirdExpense;

                        if (sum == 2020) {
                            Console.WriteLine($"{firstExpense * secondExpense * thirdExpense}");
                        }
                    }
                }
            }
        }
    }
}
