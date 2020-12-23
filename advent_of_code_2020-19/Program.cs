using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_19
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt");
            var rulesAsString = input.Split(new string[] { Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)[0].Split(Environment.NewLine).ToList();
            var messages = input.Split(new string[] { Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)[1].Split(Environment.NewLine);

            var endRules = rulesAsString.Where(x => x.Contains("\""));

            var workedOutRules = new Dictionary<int, List<string>>();
            foreach (var rule in endRules) {
                workedOutRules.Add(int.Parse(rule.Split(": ")[0]), new List<string> { rule.Split(": ")[1].Replace("\"", "")} );
            }

            rulesAsString.RemoveAll(x => endRules.Contains(x));
            var rules = rulesAsString.Select(Rule.FromString).ToList();

            Console.WriteLine(PartOne(workedOutRules.ToDictionary(entry => entry.Key, entry => entry.Value), new List<Rule>(rules), messages));
            Console.WriteLine(PartTwo(workedOutRules.ToDictionary(entry => entry.Key, entry => entry.Value), new List<Rule>(rules), messages));
        }

        static int PartOne(Dictionary<int, List<string>> workedOutRules, List<Rule> rules, string[] messages) {
            WorkOutRules(workedOutRules, rules);

            var hashedRuleZero = new HashSet<string>(workedOutRules[0]);
            return messages.Count(x => hashedRuleZero.Contains(x));
        }

        static int PartTwo(Dictionary<int, List<string>> workedOutRules, List<Rule> rules, string[] messages) {
            WorkOutRules(workedOutRules, rules);

            // K, a bit ugly...
            // Rule 0 = 8 11
            // 8 = 42 | 8
            //      This means 8 is always a multitude of 42
            //          42, or 42 42, or 42 42 42 etc.
            // 11 = 42 31 | 42 11 31
            //      This means 11 is always x 42, followed by x 31
            //          42 31, or 42 42 31 31, or 42 42 42 31 31 31 etc.
            // So, 0 is a y 42, followed by x 42, followed by x 31
            // All messages are multiples of 8 characters.
            // So, for 24 characters, it has to be 42 42 31
            // For 32 characters, 42 42 42 31
            // For 40, either 42 42 42 42 31, or 42 42 42 31 31
            // Etc.
            // So, this huuuuuge switch statement is basically checking for the above pattern,
            // based on the number of characters of the message.

            var fourtyTwo = workedOutRules[42].ToHashSet();
            var thirtyOne = workedOutRules[31].ToHashSet();

            int validCount = 0;
            foreach (var message in messages) {
                switch (message.Length) {
                    case 24:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (thirtyOne.Contains(message.Substring(16, 8))) {
                                    validCount++;
                                }
                            }
                        }
                        break;
                    case 32:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (thirtyOne.Contains(message.Substring(24, 8))) {
                                        validCount++;
                                    }
                                }
                            }
                        }
                        break;
                    case 40:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))
                                        || thirtyOne.Contains(message.Substring(24, 8))) {
                                            if (thirtyOne.Contains(message.Substring(32, 8))) {
                                                validCount++;
                                            }
                                    }
                                }
                            }
                        }
                        break;
                    case 48:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))
                                            || thirtyOne.Contains(message.Substring(32, 8))) {
                                                if (thirtyOne.Contains(message.Substring(40, 8))) {
                                                    validCount++;
                                                }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 56:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))
                                                || thirtyOne.Contains(message.Substring(40, 8))) {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        validCount++;
                                                    }
                                            }
                                        } else {
                                            if (thirtyOne.Contains(message.Substring(32, 8))) {
                                                if (thirtyOne.Contains(message.Substring(40, 8))) {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        validCount++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 64:
                        if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))) {
                                                if (fourtyTwo.Contains(message.Substring(48, 8))
                                                    || thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            validCount++;
                                                        }
                                                }
                                            } else {
                                                if (thirtyOne.Contains(message.Substring(40, 8))) {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            validCount++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 72:
                       if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))) {
                                                if (fourtyTwo.Contains(message.Substring(48, 8))) {
                                                    if (fourtyTwo.Contains(message.Substring(56, 8))
                                                        || thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                validCount++;
                                                            }
                                                    }
                                                } else {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                validCount++;
                                                            }
                                                        }
                                                    }
                                                }
                                            } else {
                                                if (thirtyOne.Contains(message.Substring(40, 8))) {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                validCount++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 80:
                       if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))) {
                                                if (fourtyTwo.Contains(message.Substring(48, 8))) {
                                                    if (fourtyTwo.Contains(message.Substring(56, 8))) {
                                                        if (fourtyTwo.Contains(message.Substring(64, 8))
                                                            || thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    validCount++;
                                                                }
                                                        }
                                                    } else {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    validCount++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                } else {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    validCount++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 88:
                       if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))) {
                                                if (fourtyTwo.Contains(message.Substring(48, 8))) {
                                                    if (fourtyTwo.Contains(message.Substring(56, 8))) {
                                                        if (fourtyTwo.Contains(message.Substring(64, 8))) {
                                                            if (fourtyTwo.Contains(message.Substring(72, 8))
                                                                || thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        validCount++;
                                                                    }
                                                            }
                                                        } else {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        validCount++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } else {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        validCount++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                } else {
                                                    if (thirtyOne.Contains(message.Substring(48, 8))) {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        validCount++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 96:
                       if (fourtyTwo.Contains(message.Substring(0, 8))) {
                            if (fourtyTwo.Contains(message.Substring(8, 8))) {
                                if (fourtyTwo.Contains(message.Substring(16, 8))) {
                                    if (fourtyTwo.Contains(message.Substring(24, 8))) {
                                        if (fourtyTwo.Contains(message.Substring(32, 8))) {
                                            if (fourtyTwo.Contains(message.Substring(40, 8))) {
                                                if (fourtyTwo.Contains(message.Substring(48, 8))) {
                                                    if (fourtyTwo.Contains(message.Substring(56, 8))) {
                                                        if (fourtyTwo.Contains(message.Substring(64, 8))) {
                                                            if (fourtyTwo.Contains(message.Substring(72, 8))) {
                                                                if (fourtyTwo.Contains(message.Substring(80, 8))
                                                                    || thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        if (thirtyOne.Contains(message.Substring(88, 8))) {
                                                                            validCount++;
                                                                        }
                                                                }
                                                            } else {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        if (thirtyOne.Contains(message.Substring(88, 8))) {
                                                                            validCount++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        } else {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        if (thirtyOne.Contains(message.Substring(88, 8))) {
                                                                            validCount++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } else {
                                                        if (thirtyOne.Contains(message.Substring(56, 8))) {
                                                            if (thirtyOne.Contains(message.Substring(64, 8))) {
                                                                if (thirtyOne.Contains(message.Substring(72, 8))) {
                                                                    if (thirtyOne.Contains(message.Substring(80, 8))) {
                                                                        if (thirtyOne.Contains(message.Substring(88, 8))) {
                                                                            validCount++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            return validCount;
        }

        static void WorkOutRules(Dictionary<int, List<string>> workedOutRules, List<Rule> rules) {
            while (rules.Count > 0) {
                foreach (var rule in rules) {

                    bool allContained = true;
                    foreach (var ruleNumber in rule.FlattenedContents) {
                        if (!workedOutRules.ContainsKey(ruleNumber)) {
                            allContained = false;
                            break;
                        }
                    }

                    if (allContained) {
                        List<List<string>> workedOutRuleContents = new List<List<string>>();
                        foreach (var list in rule.RuleContents) {
                            var result = workedOutRules[list[0]];
                            List<string> totalIntermediateCombinations = new List<string>();
                            foreach (var ruleNumber in list.Skip(1)) {
                                foreach (var r in result) {
                                    List<string> intermediateCombinations = new List<string>();

                                    foreach (var q in workedOutRules[ruleNumber]) {
                                        intermediateCombinations.Add(r + q);
                                    }

                                    totalIntermediateCombinations.AddRange(intermediateCombinations);
                                }
                                result = totalIntermediateCombinations.ToList();
                                totalIntermediateCombinations = new List<string>();
                            }
                            workedOutRuleContents.Add(result);
                        }

                        workedOutRules.Add(rule.RuleNumber, workedOutRuleContents.SelectMany(x => x).Distinct().ToList());
                    }
                }

                rules = rules.Where(x => !workedOutRules.ContainsKey(x.RuleNumber)).ToList();
            }
        }
    }
}