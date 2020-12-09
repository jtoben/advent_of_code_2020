using System;
using System.Collections.Generic;

namespace advent_of_code_2020_7
{
    public class TreeNode
    {
        public TreeNode(string name) {
            Name = name;
        }

        public string Name { get; }
        public Dictionary<TreeNode, int> Children {get; } = new Dictionary<TreeNode, int>();
        public TreeNode Parent {get; private set; }

        public TreeNode AddChild(string name, int number) {
            var node = new TreeNode(name) {
                Parent = this
            };

            Children.Add(node, number);
            return node;
        }

        public int GetNumberOfBags() {
            int numberOfBags = 1;

            foreach (var kvp in Children) {
                numberOfBags += kvp.Key.GetNumberOfBags() * kvp.Value;
            }

            return numberOfBags;
        }

        public List<TreeNode> Flatten() {

            List<TreeNode> flattenedTree = new List<TreeNode> {
                this
            };

            foreach (var kvp in Children) {
                flattenedTree.AddRange(kvp.Key.Flatten());
            }

            return flattenedTree;
        }
    }
}