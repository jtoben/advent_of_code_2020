using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020_11
{
    class Program
    {
        private static int _xLength;
        private static int _yLength;

        static void Main(string[] args) {
            var lines = File.ReadAllLines("input.txt");
            _xLength = lines[0].Length;
            _yLength = lines.Length;

            char[,] seatingMap = new char[_xLength, _yLength];
            for (int y = 0; y < _yLength; y++) {
                for (int x = 0; x < _xLength; x++) {
                    seatingMap[x,y] = lines[y][x];
                }
            }

            Console.WriteLine(PartOne(Copy(seatingMap)));
            Console.WriteLine(PartTwo(Copy(seatingMap)));
        }

        static int PartOne(char[,] seatingMap) {
            while(true) {
                int changes = UpdateSeatingMap(seatingMap, maximumNeighbours: 4, lineOfSightLength: 1);
                if (changes == 0) {
                    break;
                }
            }

            return CountOccupiedSeats(seatingMap);
        }

        static int PartTwo(char[,] seatingMap) {
            while(true) {
                int changes = UpdateSeatingMap(seatingMap, maximumNeighbours: 5, lineOfSightLength: -1);
                if (changes == 0) {
                    break;
                }
            }

            return CountOccupiedSeats(seatingMap);
        }

        static int UpdateSeatingMap(char[,] seatingMap, int maximumNeighbours, int lineOfSightLength) {
            List<(int X, int Y)> seatingChanges = new List<(int X, int Y)>();
            for (int x = 0; x < _xLength; x++) {
                for (int y = 0; y < _yLength; y++) {
                    if (seatingMap[x, y] == '.') {
                        continue;
                    }

                    int occupiedNeighboursCount = GetOccupiedNeighboursCount(seatingMap, x, y, lineOfSightLength);
                    switch (seatingMap[x, y]) {
                        case 'L':
                            if (occupiedNeighboursCount == 0) {
                                seatingChanges.Add((x, y));
                            }
                            break;
                        case '#':
                            if (occupiedNeighboursCount >= maximumNeighbours) {
                                seatingChanges.Add((x, y));
                            }
                            break;
                    }
                }
            }

            foreach (var seatingChange in seatingChanges) {
                var newChar = seatingMap[seatingChange.X, seatingChange.Y] == 'L' ? '#' : 'L';
                seatingMap[seatingChange.X, seatingChange.Y] = newChar;
            }

            return seatingChanges.Count;
        }

        static int GetOccupiedNeighboursCount(char[,] seatingMap, int x, int y, int lineOfSightLength) {
            var directions = new List<(int X, int Y)> {
                (0, -1),
                (1, -1),
                (1, 0),
                (1, 1),
                (0, 1),
                (-1, 1),
                (-1, 0),
                (-1, -1)
            };

            int occupiedSeats = 0;
            foreach (var direction in directions) {

                var previousPosition = (X: x, Y: y);
                int searchLength = 0;
                while (true) {
                    if (searchLength == lineOfSightLength) {
                        break;
                    }
                    var nextPosition = (X: previousPosition.X + direction.X, Y: previousPosition.Y + direction.Y);

                    if (nextPosition.X < 0 || nextPosition.X >= _xLength || nextPosition.Y < 0 || nextPosition.Y >= _yLength) {
                        break;
                    }

                    if (seatingMap[nextPosition.X, nextPosition.Y] == '#') {
                        occupiedSeats++;
                        break;
                    } else if (seatingMap[nextPosition.X, nextPosition.Y] == 'L') {
                        break;
                    }

                    previousPosition = nextPosition;
                    searchLength++;
                }
            }

            return occupiedSeats;
        }

        static char[,] Copy(char[,] seatingMap) {
            char[,] copy = new char[_xLength, _yLength];
            for (int x = 0; x < _xLength; x++) {
                for (int y = 0; y < _yLength; y++) {
                    copy[x, y] = seatingMap[x, y];
                }
            }

            return copy;
        }

        static int CountOccupiedSeats(char[,] seatingMap) {
            int occupiedSeats = 0;
            for (int x = 0; x < _xLength; x++) {
                for (int y = 0; y < _yLength; y++) {
                    if (seatingMap[x,y] == '#') {
                        occupiedSeats++;
                    }
                }
            }

            return occupiedSeats;
        }
    }
}