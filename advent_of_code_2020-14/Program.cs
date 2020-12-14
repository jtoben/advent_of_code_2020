using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_14
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt");

            Console.WriteLine(PartOne(instructions));
            Console.WriteLine(PartTwo(instructions));
        }

        static long PartOne(string[] instructions) {
            string mask = new string('X', 36);

            Dictionary<long, long> valuesByAddresses = new Dictionary<long, long>();
            foreach (var instruction in instructions) {
                if (instruction[1] == 'a') {
                    mask = instruction.Split('=')[1].Replace(" ", "");
                    continue;
                }

                long address = long.Parse(instruction.Split('=')[0].Replace("mem[", "").Replace("]", ""));
                long value = long.Parse(instruction.Split('=')[1].Replace(" ", ""));

                var valueInBits = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();;
                for (int i = 0; i < mask.Length; i++) {
                    if (mask[i] == 'X') {
                        continue;
                    }

                    valueInBits[i] = mask[i];
                }
                valuesByAddresses[address] = Convert.ToInt64(new string(valueInBits), 2);
            }

            return valuesByAddresses.Select(x => x.Value).Sum();
        }

        static long PartTwo(string[] instructions) {
            string mask = new string('X', 36);

            Dictionary<long, long> valuesByAddresses = new Dictionary<long, long>();
            foreach (var instruction in instructions) {
                if (instruction[1] == 'a') {
                    mask = instruction.Split('=')[1].Replace(" ", "");
                    continue;
                }

                long address = long.Parse(instruction.Split('=')[0].Replace("mem[", "").Replace("]", ""));
                long value = long.Parse(instruction.Split('=')[1].Replace(" ", ""));

                var addressInBits = Convert.ToString(address, 2).PadLeft(36, '0').ToCharArray();
                for (int i = 0; i < mask.Length; i++) {
                    if (mask[i] == '0') {
                        continue;
                    }

                    addressInBits[i] = mask[i];
                }

                List<long> addressVariations = new List<long>();

                Stack<string> addressStrings = new Stack<string>();
                addressStrings.Push(new string(addressInBits));
                while (addressStrings.Count > 0) {
                    var currentAddress = addressStrings.Pop();

                    if (!currentAddress.Contains('X')) {
                        addressVariations.Add(Convert.ToInt64(new string(currentAddress), 2));
                        continue;
                    }

                    for (int i = 0; i < currentAddress.Length; i++) {
                        if (currentAddress[i] != 'X') {
                            continue;
                        }

                        var zeroAddress = currentAddress.ToArray();
                        var oneAddress = currentAddress.ToArray();
                        zeroAddress[i] = '0';
                        addressStrings.Push(new string(zeroAddress));
                        oneAddress[i] = '1';
                        addressStrings.Push(new string(oneAddress));

                        break;
                    }
                }

                foreach (var variation in addressVariations) {
                    valuesByAddresses[variation] = value;
                }
            }

            return valuesByAddresses.Select(x => x.Value).Sum();
        }
    }
}