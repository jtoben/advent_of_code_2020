using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_18
{
    class Program
    {
        static void Main()
        {
            var expressions = File.ReadAllLines("input.txt").Select(x => x.Replace(" ", "")).ToArray();

            Console.WriteLine(PartOne(expressions));
            Console.WriteLine(PartTwo(expressions));
        }

        static long PartOne(string[] expressions) {
            long result = 0L;
            foreach (var expression in expressions) {
                result += FindExpression(expression, withPrecendence: false);
            }

            return result;
        }

        static long PartTwo(string[] expressions) {
            long result = 0L;
            foreach (var expression in expressions) {
                result += FindExpression(expression, withPrecendence: true);
            }

            return result;
        }

        static long FindExpression(string line, bool withPrecendence) {
            var currentExpression = "";
            int numberOfOpenBrackets = 0;
            for (int i = 0; i < line.Length; i++) {
                switch (line[i]) {
                    case '(':
                        numberOfOpenBrackets++;
                        if (numberOfOpenBrackets == 1) {
                            currentExpression += FindExpression(line[(i + 1) .. ^0], withPrecendence);
                        }
                        break;
                    case ')':
                        numberOfOpenBrackets--;
                        if (numberOfOpenBrackets < 0) {
                            if (withPrecendence) {
                                return CalculateWithPrecedenceRules(currentExpression);
                            } else {
                                return Calculate(currentExpression);
                            }
                        }
                        break;
                    default:
                        if (numberOfOpenBrackets == 0) {
                            currentExpression += line[i];
                        }
                        break;
                }
            }
            if (withPrecendence) {
                return CalculateWithPrecedenceRules(currentExpression);
            } else {
                return Calculate(currentExpression);
            }
        }

        static long CalculateWithPrecedenceRules(string expression) {
            return expression
                .Split("*")
                .Select(x => x.Split("+").Select(long.Parse).Sum())
                .Aggregate((i, j) => i * j);
        }

        static long Calculate(string expression) {
            var pieces = expression.Replace("*", " * ").Replace("+", " + ").Split(" ");

            long result = 0;
            var operatorChar = "+";
            foreach (var piece in pieces) {
                switch (piece) {
                    case "+":
                    case "*":
                        operatorChar = piece;
                        break;
                    default:
                        switch (operatorChar) {
                            case "+":
                            result += long.Parse(piece.ToString());
                            break;
                            case "*":
                            result *= long.Parse(piece.ToString());
                            break;
                        }
                        break;
                }
            }
            return result;
        }
    }
}
