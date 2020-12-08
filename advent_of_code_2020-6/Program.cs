using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] groups = File.ReadAllText("input.txt").Split(new string[] { Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(PartOne(groups));
            Console.WriteLine(PartTwo(groups));
        }

        static int PartOne(string[] groups) {
            List<int> uniqueAnswersPerGroup = new List<int>();
            foreach (var group in groups) {

                HashSet<char> uniqueCharacters = new HashSet<char>();
                foreach (var answer in group.Split(Environment.NewLine)) {
                    foreach (var c in answer) {
                        uniqueCharacters.Add(c);
                    }
                }

                uniqueAnswersPerGroup.Add(uniqueCharacters.Count);
            }

            return uniqueAnswersPerGroup.Sum();
        }

        static int PartTwo(string[] groups) {
            List<int> uniqueUnanimousAnswersPerGroup = new List<int>();
            foreach (var group in groups) {
                var charCount = new Dictionary<char, int> {
                    {'a', 0},
                    { 'b', 0},
                    { 'c', 0},
                    { 'd', 0},
                    { 'e', 0},
                    { 'f', 0},
                    { 'g', 0},
                    { 'h', 0},
                    { 'i', 0},
                    { 'j', 0},
                    { 'k', 0},
                    { 'l', 0},
                    { 'm', 0},
                    { 'n', 0},
                    { 'o', 0},
                    { 'p', 0},
                    { 'q', 0},
                    { 'r', 0},
                    { 's', 0},
                    { 't', 0},
                    { 'u', 0},
                    { 'v', 0},
                    { 'w', 0},
                    { 'x', 0},
                    { 'y', 0},
                    { 'z', 0}
                };

                var answers = group.Split(Environment.NewLine);
                foreach (var answer in answers) {
                    foreach (var c in answer) {
                        charCount[c]++;
                    }
                }
                uniqueUnanimousAnswersPerGroup.Add(charCount.Count(x => x.Value == answers.Length));
            }

            return uniqueUnanimousAnswersPerGroup.Sum();
        }
    }
}
