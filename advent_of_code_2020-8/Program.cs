using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt");
            
            Console.WriteLine(PartOne(instructions));
            Console.WriteLine(PartTwo(instructions));
        }

        static int PartOne(string[] instructions) {
            return RunCalculation(instructions).Accumulator;
        }

        static int PartTwo(string[] instructions) {
            for (int i = 0; i < instructions.Length; i++) {

                var copy = instructions.ToArray();
                var instruction = copy[i];

                switch (instruction.Split(' ')[0]) {
                    case "nop":
                        instruction = instruction.Replace("nop", "jmp");
                        break;
                    case "jmp":
                        instruction = instruction.Replace("jmp", "nop");
                        break;
                    case "acc":
                        continue;
                }

                copy[i] = instruction;

                var result = RunCalculation(copy);
                if (result.Success) {
                    return result.Accumulator;
                }

            }
            return -1;
        }

        static (bool Success, int Accumulator) RunCalculation(string[] instructions) {
            int accumulator = 0;
            int pointer = 0;

            List<int> visitedInstructions = new List<int>();
            bool ranSuccessFul = false;
            while (true) {
                if (visitedInstructions.Contains(pointer)) {
                    break;
                } 

                if (pointer == instructions.Length) {
                    ranSuccessFul = true;
                    break;
                }

                visitedInstructions.Add(pointer);

                var instruction = instructions[pointer].Split(' ')[0];
                int parameter = int.Parse(instructions[pointer].Split(' ')[1]);

                switch (instruction) {
                    case "nop":
                        pointer++;
                        continue;
                    case "acc":
                        accumulator += parameter;
                        pointer++;
                        continue;
                    case "jmp":
                        pointer += parameter;
                        continue;
                }
            }

            return (ranSuccessFul, accumulator);
        }
    }
}
