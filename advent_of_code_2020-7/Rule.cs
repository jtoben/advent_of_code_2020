using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_7
{
    public class Rule
    {
        public string Name { get; set; }
        public Dictionary<string, int> Contents { get; set; } = new Dictionary<string, int>();

        public static Rule FromString(string ruleAsString) {
            var name = GetName(ruleAsString);
            var rawContent = ruleAsString.Split("contain")[1];
            Dictionary<string, int> contents = new Dictionary<string, int>();
            if (rawContent != " no other bags") {
                foreach (var contentPart in rawContent.Split(',')) {
                    contents.Add(GetNameFromContentPart(contentPart), GetNumberFromContentPart(contentPart));
                }
            }

            Rule rule = new Rule {
                Name = name,
                Contents = contents
            };

            return rule;
        }

        private static string GetName(string ruleAsString) {
            var name = ruleAsString.Split("contain")[0];
            name = string.Join(' ', name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2));

            return name;
        }

        private static string GetNameFromContentPart(string contentPart) {
            return string.Join(' ', contentPart.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Take(2));
        }

        private static int GetNumberFromContentPart(string contentPart) {
            return int.Parse(contentPart.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
        }
    }
}