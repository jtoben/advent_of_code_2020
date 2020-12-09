using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rule> rules = File.ReadAllLines("input.txt")
                .Select(x => x[0 .. ^1])
                .Select(Rule.FromString).ToList();

            Console.WriteLine(PartOne(rules));
            Console.WriteLine(PartTwo(rules));
        }

        static int PartOne(List<Rule> rules) {
            var rootRules = GetRootRules(rules);

            List<TreeNode> rootNodes = new List<TreeNode>();
            foreach (var rootRule in rootRules) {
                var rootNode = new TreeNode(rootRule.Name);
                AddChildren(rootNode, rules);

                rootNodes.Add(rootNode);
            }
            HashSet<string> visitedNodeNames = new HashSet<string>();
            foreach (var rootNode in rootNodes) {
                var flattenedTree = rootNode.Flatten();

                var shinyGoldNodes = flattenedTree.Where(x => x.Name == "shiny gold");
                foreach (var shinyGoldNode in shinyGoldNodes) {
                    var parent = shinyGoldNode.Parent;
                    while (parent != null) {
                        visitedNodeNames.Add(parent.Name);
                        parent = parent.Parent;
                    }
                }
            }

            return visitedNodeNames.Count;
        }

        static int PartTwo(List<Rule> rules) {

            var rootRule = rules.First(x => x.Name == "shiny gold");
            var rootNode = new TreeNode(rootRule.Name);
            AddChildren(rootNode, rules);

            return rootNode.GetNumberOfBags() - 1;
        }

        static void AddChildren(TreeNode currentNode, List<Rule> rules) {
            var currentRule = rules.First(x => x.Name == currentNode.Name);

            foreach (var kvp in currentRule.Contents) {
                currentNode.AddChild(kvp.Key, kvp.Value);
            }

            foreach (var kvp in currentNode.Children) {
                AddChildren(kvp.Key, rules);
            }
        }

        static List<Rule> GetRootRules(List<Rule> rules) {
            List<Rule> rootRules = new List<Rule>();
            for (int i = 0; i < rules.Count; i++) {
                bool isRoot = true;
                var current = rules[i];
                for (int j = 0; j < rules.Count; j++) {
                    var next = rules[j];

                    if (next.Contents.Keys.Contains(current.Name)) {
                        isRoot = false;
                        break;
                    }
                }

                if (isRoot) {
                    rootRules.Add(current);
                }
            }

            return rootRules;
        }
    }
}
