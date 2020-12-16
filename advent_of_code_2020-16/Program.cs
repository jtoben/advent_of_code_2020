using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_16
{
    class Program
    {
        static void Main(string[] args)
        {
            var allText = File.ReadAllText("input.txt");
            var stringRules = allText.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0].Split(Environment.NewLine);
            var rules = stringRules
                .Select(x => x.Split(": ")[1])
                .SelectMany(x => {
                    var left = x.Split(" or ")[0];
                    var right = x.Split(" or ")[1];

                    var firstRange =  (Minimum: int.Parse(left.Split("-")[0]), Maximum: int.Parse(left.Split("-")[1]));
                    var secondRange =  (Minimum: int.Parse(right.Split("-")[0]), Maximum: int.Parse(right.Split("-")[1]));

                    return new List<(int Minimum, int Maximum)> {firstRange, secondRange };
                }).ToList();
            var nearbyTickets = allText.Split("nearby tickets:" + Environment.NewLine)[1].Split(Environment.NewLine);
            var departeRules = stringRules.Where(x => x.StartsWith("departure")).ToList();
            var myTicket = allText.Split("your ticket:" + Environment.NewLine)[1].Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0];

            Console.WriteLine(PartOne(rules, nearbyTickets));
            Console.WriteLine(PartTwo(rules, nearbyTickets, departeRules, myTicket));
        }

        static int PartOne(List<(int Minimum, int Maximum)> rules, string[] nearbyTickets) {
            var failedTickets = GetFailedTickets(rules, nearbyTickets);
            return failedTickets.Select(x => x.Value).Sum();
        }

        static long PartTwo(List<(int Minimum, int Maximum)> rules, string[] nearbyTickets, List<string> departureRules, string myTicket) {
            var failedTickets = GetFailedTickets(rules, nearbyTickets);
            var validTickets = nearbyTickets.Where(x => !failedTickets.Keys.Contains(x));

            Dictionary<int, List<(int Minimum, int Maximum)>> rulesPerValue = new Dictionary<int, List<(int Minimum, int Maximum)>>();
            for (int i = 0; i < nearbyTickets[0].Split(',').Length; i++) {

                var possibleRules = new List<(int Minimum, int Maximum)>(rules);

                foreach (var ticket in validTickets) {
                    var valueToCheck = int.Parse(ticket.Split(',')[i]);

                    var validRules = new List<(int Minimum, int Maximum)>();
                    for (int j = 0; j < possibleRules.Count - 1; j += 2) {
                        var firstRule = possibleRules[j];
                        var secondRule = possibleRules[j + 1];

                        if ((valueToCheck >= firstRule.Minimum && valueToCheck <= firstRule.Maximum)
                            || (valueToCheck >= secondRule.Minimum && valueToCheck <= secondRule.Maximum)) {
                            validRules.Add(firstRule);
                            validRules.Add(secondRule);
                        }
                    }

                    possibleRules = new List<(int Minimum, int Maximum)>(validRules);
                }

                rulesPerValue.Add(i, possibleRules);
            }

            var orderedRulesPerValue = rulesPerValue.OrderBy(x => x.Value.Count).ToList();
            for (int i = 0; i < orderedRulesPerValue.Count; i++) {
                for (int j = i + 1; j < orderedRulesPerValue.Count; j++) {
                    for (int k = 0; k < orderedRulesPerValue[j].Value.Count; k += 2) {
                        if (orderedRulesPerValue[j].Value[k] == orderedRulesPerValue[i].Value[0] && orderedRulesPerValue[j].Value[k + 1] == orderedRulesPerValue[i].Value[1]) {
                            orderedRulesPerValue[j].Value.RemoveRange(k, 2);
                            break;
                        }
                    }
                }
            }


            Dictionary<int, int> departureIndexToRule = new Dictionary<int, int>();
            foreach (var rule in rulesPerValue) {
                var rulesAsString = rule.Value.Select(x => $"{x.Minimum}-{x.Maximum}").Aggregate((i, j) => i + " or " + j);
                var departureIndex = departureRules.FindIndex(x => x.Contains(rulesAsString));
                if (departureIndex != -1) {
                    departureIndexToRule.Add(departureIndex, rule.Key);
                }
            }

            long sum = 1L;
            for (int i = 0; i < departureRules.Count; i++) {
                var myTicketValues = myTicket.Split(',').Select(int.Parse).ToArray();
                sum *= myTicketValues[departureIndexToRule[i]];
            }

            return sum;
        }

        static Dictionary<string, int> GetFailedTickets(List<(int Minimum, int Maximum)> rules, string[] otherTickets) {
            Dictionary<string, int> failedTickets = new Dictionary<string, int>();
            foreach (var ticket in otherTickets) {
                var values = ticket.Split(',').Select(int.Parse);
                List<int> failedValuesForTicket = new List<int>();
                int failedValue = -1;
                foreach (var value in values) {

                    bool success = false;
                    foreach (var (minimum, maximum) in rules) {
                        if (value >= minimum && value <= maximum) {
                            success = true;
                            break;
                        }
                    }

                    if (!success) {
                        failedValue = value;
                        break;
                    }
                }

                if (failedValue != -1) {
                    failedTickets.Add(ticket, failedValue);
                }
            }

            return failedTickets;
        }
    }
}
