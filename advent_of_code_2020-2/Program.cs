using System;
using System.IO;
using System.Linq;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            int validPasswords = 0;

            foreach (var line in lines) {
                var rule = line.Split(":")[0];
                var password = line.Split(":")[1];

                var countRule = rule.Split(" ")[0];

                char requiredChar = rule.Split(" ")[1][0];
                int minCount = int.Parse(countRule.Split("-")[0]);
                int maxCount = int.Parse(countRule.Split("-")[1]);

                if (password.Length <= maxCount) {
                    continue;
                }

                bool firstValid = password[minCount] == requiredChar;
                bool secondValid = password[maxCount] == requiredChar;

                validPasswords += PartTwo(minCount, maxCount, password, requiredChar);
            }

            Console.WriteLine(validPasswords);
        }

        static int PartOne(int minCount, int maxCount, string password, char requiredChar) {

            int numberOfCharsInPassword = password.Count(x => x == requiredChar);

            return numberOfCharsInPassword >= minCount && numberOfCharsInPassword <= maxCount ? 1: 0;
        }

        static int PartTwo(int minCount, int maxCount, string password, char requiredChar) {
            if (password.Length <= maxCount) {
                return 0;
            }

            bool firstValid = password[minCount] == requiredChar;
            bool secondValid = password[maxCount] == requiredChar;

            return firstValid ^ secondValid ? 1 : 0;
        }
    }
}
