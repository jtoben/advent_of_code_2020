using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_13
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            int startTimeStamp = int.Parse(lines[0]);

            Console.WriteLine(PartOne(startTimeStamp, lines[1]));
            Console.WriteLine(PartTwo(lines[1]));
        }

        static int PartOne(int startTimeStamp, string busIdentifiersString) {

            var validBusIdentifiers = busIdentifiersString.Split(',').Where(x => x != "x").Select(int.Parse);
            Dictionary<int, int> waitTimesPerBus = new Dictionary<int, int>();
            foreach (var busIdentifier in validBusIdentifiers) {

                var timeSinceLastDeparture = startTimeStamp % busIdentifier;
                waitTimesPerBus.Add(busIdentifier, busIdentifier - timeSinceLastDeparture);
            }

            return waitTimesPerBus.OrderBy(x => x.Value).Select(x => x.Key * x.Value).First();
        }

        static long PartTwo(string busIdentifiersString) {

            Dictionary<int, int> busIdentifiersWithOffset = new Dictionary<int, int>();
            var busIdentifiers = busIdentifiersString.Split(',');
            for (int i = 0; i < busIdentifiers.Length; i++) {
                if (busIdentifiers[i] == "x") {
                    continue;
                }

                busIdentifiersWithOffset.Add(int.Parse(busIdentifiers[i]),  i);
            }
            
            long timeStamp = 0;
            long stepSize = busIdentifiersWithOffset.First().Key;

            int numberOfMatchingIdentifiers = 2;
            while (true) {
                timeStamp += stepSize;
                var busIdentifiersToCheck = busIdentifiersWithOffset.Take(numberOfMatchingIdentifiers);

                bool success = true;
                foreach (var kvp in busIdentifiersToCheck) {
                    if ((timeStamp + kvp.Value)  % kvp.Key != 0) { // kvp.Value = offset, kvp.Key = busIdentifier
                        success = false;
                        break;
                    }
                }

                if (success){
                    if (numberOfMatchingIdentifiers == busIdentifiersWithOffset.Count) {
                        break;
                    } else {
                        numberOfMatchingIdentifiers++;
                        stepSize = busIdentifiersToCheck.Select(x => (long)x.Key).Aggregate((x, y) => x * y);
                    }
                }
            }

            return timeStamp;
        }
    }
}