using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var jolts = File.ReadAllLines("input.txt").Select(int.Parse).OrderBy(x => x).ToList();
            jolts.Add(jolts.Max() + 3);

            Console.WriteLine(PartOne(new List<int>(jolts)));
            Console.WriteLine(PartTwo(new List<int>(jolts)));
        }

        static int PartOne(List<int> jolts) {
            Dictionary<int, int> stepCounts = new Dictionary<int, int> {
                {1, 0},
                {3, 0}
            };

            int previousJolt = 0;
            foreach (var jolt in jolts) {
                var difference = jolt - previousJolt;
                stepCounts[difference]++;

                previousJolt = jolt;
            }

            return stepCounts[1] * stepCounts[3];
        }

        static long PartTwo(List<int> jolts) {
            jolts.Insert(0, 0);
            int maxNumber = jolts.Max();

            Dictionary<int, List<int>> optionsPerJolt = new Dictionary<int, List<int>>();
            for (int i = 0; i < jolts.Count; i++) {
                var options = jolts.Skip(i + 1).TakeWhile(x => x - jolts[i] <= 3).ToList();
                optionsPerJolt[jolts[i]] = options;
            }

            int highestJolt = -1;
            Dictionary<int, bool> visitedJunctions = new Dictionary<int, bool>();
            Dictionary<int, long> numberOfJunctionsPerJolt = new Dictionary<int, long>();
            List<int> sequenceOfJolts = new List<int> {
                0
            };

            Stack<int> stack = new Stack<int>(); // With a stack, we do a DFS.
            foreach (var option in optionsPerJolt[0]) {
                stack.Push(option);
            }
            while (stack.Count > 0) {
                int current = stack.Pop();
                if (current > highestJolt) {
                    sequenceOfJolts.Add(current);
                } else { 
                    // Whenever currentNumber is not highest, we've gone back in the sequence.
                    // Meaning we've hit a junction.
                    sequenceOfJolts = sequenceOfJolts.Where(x => x < current).ToList();
                    sequenceOfJolts.Add(current);

                    int junction = sequenceOfJolts.Last(x => x < current);
                    var nextNumber = jolts.First(x => x > current);
                    visitedJunctions.TryAdd(junction, false);

                    var nextJunctions = visitedJunctions.Where(x => x.Key > junction).OrderByDescending(x => x.Key);
                    if (nextJunctions.Count() == 0) {
                        numberOfJunctionsPerJolt.TryAdd(nextNumber, 1);
                    } else {
                        CalculateJunctions(nextJunctions, optionsPerJolt, numberOfJunctionsPerJolt);
                    }
                }
                highestJolt = current;

                if (numberOfJunctionsPerJolt.ContainsKey(current)) {
                    continue;
                }

                var options = optionsPerJolt[current];
                foreach (var option in options) {
                    stack.Push(option);
                }
            }
            
            var orderedVisitedJunctions = visitedJunctions.OrderByDescending(x => x.Key);
            CalculateJunctions(orderedVisitedJunctions, optionsPerJolt, numberOfJunctionsPerJolt);

            return numberOfJunctionsPerJolt.Select(x => x.Value).Max();
        }

        static void CalculateJunctions(IOrderedEnumerable<KeyValuePair<int, bool>> orderedJunctions,
            Dictionary<int, List<int>> optionsPerJolt,
            Dictionary<int, long> numberOfJunctionsPerJolt) {
            foreach (var kvp in orderedJunctions) {
                if (!kvp.Value) {
                    // Unhandled junction.
                    var junctionOptions = optionsPerJolt[kvp.Key];
                    junctionOptions.Reverse();
                    long totalSum = 0;
                    foreach (var junctionOption in junctionOptions) {
                        if (!numberOfJunctionsPerJolt.ContainsKey(junctionOption)) {
                            var nextKey = numberOfJunctionsPerJolt.Keys.Last(x => x > junctionOption);
                            numberOfJunctionsPerJolt[junctionOption] = numberOfJunctionsPerJolt[nextKey];
                        }
                        totalSum += numberOfJunctionsPerJolt[junctionOption];
                    }
                    numberOfJunctionsPerJolt[kvp.Key] = totalSum;
                }
            }
        }
    }
}
