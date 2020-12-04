using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_4
{
    class Program
    {
        private static readonly List<string> mandatoryPropertyNames = new List<string> {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };
        
        static void Main(string[] args)
        {
            string[] passports = File.ReadAllText("input.txt").Split(new string[] { Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            List<string[]> propertiesPerPassport = new List<string[]>();
            foreach (var passport in passports) {

                var properties = passport.Split(new char[] { ' ', '\n' });
                propertiesPerPassport.Add(properties);
            }

            Console.WriteLine(PartOne(propertiesPerPassport));
            Console.WriteLine(PartTwo(propertiesPerPassport));
        }

        static int PartOne(List<string[]> propertiesPerPassport) {
            var validPassports = GetPassportsWithAllMandatoryProperties(propertiesPerPassport);
            return validPassports.Count;
        }

        static int PartTwo(List<string[]> propertiesPerPassport) {

            var validPassports = GetPassportsWithAllMandatoryProperties(propertiesPerPassport);

            int numberOfFullValidatedPassports = 0;
            foreach (var passport in validPassports) {
                bool allPropertiesValid = true;
                foreach (var property in passport) {
                    if (!IsPropertyValid(property)) {
                        allPropertiesValid = false;
                        break;
                    }
                }

                if (allPropertiesValid) {
                    numberOfFullValidatedPassports++;
                }
            }

            return numberOfFullValidatedPassports;
        }

        static List<string[]> GetPassportsWithAllMandatoryProperties(List<string[]> propertiesPerPassport) {

            List<string[]> validPassports = new List<string[]>();
            foreach (var properties in propertiesPerPassport) {

                var propertyNames = properties.Select(x => x.Split(":")[0]);

                bool validPassport = mandatoryPropertyNames.All(x => propertyNames.Contains(x));
                if (validPassport) {
                    validPassports.Add(properties);
                }
            }

            return validPassports;
        }

        static bool IsPropertyValid(string property) {

            var propertyName = property.Split(":")[0];
            var propertyValue = property.Split(":")[1];

            switch (propertyName) {
                case "byr":
                    if (int.TryParse(propertyValue, out int birthYear)) {
                        return birthYear >= 1920 && birthYear <= 2002;
                    } else {
                        return false;
                    }
                case "iyr":
                    if (int.TryParse(propertyValue, out int issueYear)) {
                        return issueYear >= 2010 && issueYear <= 2020;
                    } else {
                        return false;
                    }
                case "eyr":
                    if (int.TryParse(propertyValue, out int expirationYear)) {
                        return expirationYear >= 2020 && expirationYear <= 2030;
                    } else {
                        return false;
                    }
                case "hgt":
                    if (propertyValue.Length <= 2) {
                        return false;
                    }
                    var heightType = propertyValue.Substring(propertyValue.Length - 2);
                    var heightValue = int.Parse(propertyValue.Substring(0, propertyValue.Length - 2));
                    if (heightType == "cm") {
                        return heightValue >= 150 && heightValue <= 193;
                    } else if (heightType == "in") {
                        return heightValue >= 59 && heightValue <= 76;
                    } else {
                        return false;
                    }
                case "hcl":
                    if (propertyValue.Length != 7 || propertyValue[0] != '#') {
                        return false;
                    }
                    return propertyValue.Substring(1, propertyValue.Length - 1).All(x => "0123456789abcdef".Contains(x));
                case "ecl":
                    return new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(propertyValue);
                case "pid":
                    return propertyValue.Length == 9 && int.TryParse(propertyValue, out int redundant);
                case "cid":
                    return true;
            }

            return false;
        }
    }
}
