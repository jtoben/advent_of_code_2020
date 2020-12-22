using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2020_17
{
    class Program
    {
        static void Main()
        {
            var pocketDimension = File.ReadAllLines("input.txt");

            char[, ,] growingCube = new char[pocketDimension.Length, pocketDimension.Length, 1];

            for (int y = 0; y < growingCube.GetLength(1); y++) {
                for (int x = 0; x < growingCube.GetLength(0); x++) {
                    growingCube[x, y, 0] = pocketDimension[y][x];
                }
            }

            Console.WriteLine(PartOne(pocketDimension));
            Console.WriteLine(PartTwo(pocketDimension));
        }

        static int PartOne(string[] pocketDimension) {
            char[, ,] growingCube = new char[pocketDimension.Length, pocketDimension.Length, 1];
            for (int y = 0; y < growingCube.GetLength(1); y++) {
                for (int x = 0; x < growingCube.GetLength(0); x++) {
                    growingCube[x, y, 0] = pocketDimension[y][x];
                }
            }

            for (int i = 0; i < 6; i++) {
                List<(int X, int Y, int Z)> changingCoordinates = GetChangingCoordinates(growingCube);
                var copy = GetUpdatedCopy(growingCube, changingCoordinates);

                growingCube = new char[copy.GetLength(0), copy.GetLength(1), copy.GetLength(2)];
                for (int x = 0; x < copy.GetLength(0); x++) {
                    for (int y = 0; y < copy.GetLength(1); y++) {
                        for (int z = 0; z < copy.GetLength(2); z++) {
                            growingCube[x, y, z] = copy[x, y, z];
                        }
                    }
                }
            }

            return growingCube.Cast<char>().Count(x => x == '#');
        }

        static int PartTwo(string[] pocketDimension) {
            char[, , ,] growingSuperCube = new char[pocketDimension.Length, pocketDimension.Length, 1, 1];
            for (int y = 0; y < growingSuperCube.GetLength(1); y++) {
                for (int x = 0; x < growingSuperCube.GetLength(0); x++) {
                    growingSuperCube[x, y, 0, 0] = pocketDimension[y][x];
                }
            }

            for (int i = 0; i < 6; i++) {
                List<(int X, int Y, int Z, int W)> changingCoordinates = GetChangingCoordinates(growingSuperCube);
                var copy = GetUpdatedCopy(growingSuperCube, changingCoordinates);

                growingSuperCube = new char[copy.GetLength(0), copy.GetLength(1), copy.GetLength(2), copy.GetLength(3)];
                for (int x = 0; x < copy.GetLength(0); x++) {
                    for (int y = 0; y < copy.GetLength(1); y++) {
                        for (int z = 0; z < copy.GetLength(2); z++) {
                            for (int w = 0; w < copy.GetLength(2); w++) {
                                growingSuperCube[x, y, z, w] = copy[x, y, z, w];
                            }
                        }
                    }
                }
            }

            return growingSuperCube.Cast<char>().Count(x => x == '#');
        }

        static List<(int X, int Y, int Z)> GetChangingCoordinates(char[, ,] growingCube) {
            var changingCoordinates = new List<(int X, int Y, int Z)>();
            for (int x = -1; x < growingCube.GetLength(0) + 1; x++) {
                for (int y = -1; y < growingCube.GetLength(1) + 1; y++) {
                    for (int z = -1; z < growingCube.GetLength(2) + 1; z++) {
                        int activeNeighbours = GetNumberOfActiveNeighbours(growingCube, x, y, z);
                        switch(GetCharacterAtCoordinates(growingCube, x, y, z)) {
                            case '.':
                                if (activeNeighbours == 3) {
                                    changingCoordinates.Add((x, y, z));
                                }
                                break;
                            case '#':
                                if (activeNeighbours != 2 && activeNeighbours != 3) {
                                    changingCoordinates.Add((x, y, z));
                                }
                                break;
                        }
                    }
                }
            }

            return changingCoordinates;
        }

        static List<(int X, int Y, int Z, int W)> GetChangingCoordinates(char[, , ,] growingSuperCube) {
            var changingCoordinates = new List<(int X, int Y, int Z, int W)>();
            for (int x = -1; x < growingSuperCube.GetLength(0) + 1; x++) {
                for (int y = -1; y < growingSuperCube.GetLength(1) + 1; y++) {
                    for (int z = -1; z < growingSuperCube.GetLength(2) + 1; z++) {
                        for (int w = -1; w < growingSuperCube.GetLength(3) + 1; w++) {
                            int activeNeighbours = GetNumberOfActiveNeighbours(growingSuperCube, x, y, z, w);
                            switch(GetCharacterAtCoordinates(growingSuperCube, x, y, z, w)) {
                                case '.':
                                    if (activeNeighbours == 3) {
                                        changingCoordinates.Add((x, y, z, w));
                                    }
                                    break;
                                case '#':
                                    if (activeNeighbours != 2 && activeNeighbours != 3) {
                                        changingCoordinates.Add((x, y, z, w));
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return changingCoordinates;
        }

        static char[, ,] GetUpdatedCopy(char[, ,] growingCube, List<(int X, int Y, int Z)> changingCoordinates) {
            var copy = new char[growingCube.GetLength(0) + 2, growingCube.GetLength(1) + 2, growingCube.GetLength(2) + 2];
            for (int x = 0; x < copy.GetLength(0); x++) {
                for (int y = 0; y < copy.GetLength(1); y++) {
                    for (int z = 0; z < copy.GetLength(2); z++) {
                        copy[x, y, z] = GetCharacterAtCoordinates(growingCube, x - 1, y - 1, z - 1);
                    }
                }
            }
            foreach (var (x, y, z) in changingCoordinates) {
                var newCharacter = copy[x + 1, y + 1, z + 1] == '.' ? '#' : '.';
                copy[x + 1, y + 1, z + 1] = newCharacter;
            }

            return copy;
        }

        static char[, , ,] GetUpdatedCopy(char[, , ,] growingSuperCube, List<(int X, int Y, int Z, int W)> changingCoordinates) {
            var copy = new char[growingSuperCube.GetLength(0) + 2, growingSuperCube.GetLength(1) + 2, growingSuperCube.GetLength(2) + 2, growingSuperCube.GetLength(3) + 2];
            for (int x = 0; x < copy.GetLength(0); x++) {
                for (int y = 0; y < copy.GetLength(1); y++) {
                    for (int z = 0; z < copy.GetLength(2); z++) {
                        for (int w = 0; w < copy.GetLength(2); w++) {
                                copy[x, y, z, w] = GetCharacterAtCoordinates(growingSuperCube, x - 1, y - 1, z - 1, w - 1);
                        }
                    }
                }
            }
            foreach (var (x, y, z, w) in changingCoordinates) {
                var newCharacter = copy[x + 1, y + 1, z + 1, w + 1] == '.' ? '#' : '.';
                copy[x + 1, y + 1, z + 1, w + 1] = newCharacter;
            }

            return copy;
        }

        static int GetNumberOfActiveNeighbours(char[, ,] growingCube, int x, int y, int z) {
            var directions = new List<(int X, int Y, int Z)> {
                // Corners
                (-1, -1, 1),
                (-1, 1, 1),
                (1, -1, 1),
                (1, 1, 1),
                (-1, -1, -1),
                (-1, 1, -1),
                (1, -1, -1),
                (1, 1, -1),
                // Faces
                (0, 0, 1),
                (0, 0, -1),
                (1, 0, 0),
                (-1, 0, 0),
                (0, -1, 0),
                (0, 1, 0),
                // Edges
                (-1, -1, 0),
                (-1, 1, 0),
                (1, -1, 0),
                (1, 1, 0),
                (0, -1, -1),
                (0, 1, -1),
                (0, -1, 1),
                (0, 1, 1),
                (-1, 0, -1),
                (1, 0, -1),
                (-1, 0, 1),
                (1, 0, 1)
            };

            int activeNeighbours = 0;
            foreach (var (dX, dY, dZ) in directions) {
                if (GetCharacterAtCoordinates(growingCube, x + dX, y + dY, z + dZ) == '#') {
                    activeNeighbours++;
                }
            }

            return activeNeighbours;
        }

        static int GetNumberOfActiveNeighbours(char[, , ,] growingSuperCube, int x, int y, int z, int w) {
            var directions = new List<(int X, int Y, int Z, int W)> {
                (0,0,0,1),
                (0,0,0,-1),
                (0,0,1,0),
                (0,0,1,1),
                (0,0,1,-1),
                (0,0,-1,0),
                (0,0,-1,1),
                (0,0,-1,-1),
                (0,1,0,0),
                (0,1,0,1),
                (0,1,0,-1),
                (0,1,1,0),
                (0,1,1,1),
                (0,1,1,-1),
                (0,1,-1,0),
                (0,1,-1,1),
                (0,1,-1,-1),
                (0,-1,0,0),
                (0,-1,0,1),
                (0,-1,0,-1),
                (0,-1,1,0),
                (0,-1,1,1),
                (0,-1,1,-1),
                (0,-1,-1,0),
                (0,-1,-1,1),
                (0,-1,-1,-1),
                (1,0,0,0),
                (1,0,0,1),
                (1,0,0,-1),
                (1,0,1,0),
                (1,0,1,1),
                (1,0,1,-1),
                (1,0,-1,0),
                (1,0,-1,1),
                (1,0,-1,-1),
                (1,1,0,0),
                (1,1,0,1),
                (1,1,0,-1),
                (1,1,1,0),
                (1,1,1,1),
                (1,1,1,-1),
                (1,1,-1,0),
                (1,1,-1,1),
                (1,1,-1,-1),
                (1,-1,0,0),
                (1,-1,0,1),
                (1,-1,0,-1),
                (1,-1,1,0),
                (1,-1,1,1),
                (1,-1,1,-1),
                (1,-1,-1,0),
                (1,-1,-1,1),
                (1,-1,-1,-1),
                (-1,0,0,0),
                (-1,0,0,1),
                (-1,0,0,-1),
                (-1,0,1,0),
                (-1,0,1,1),
                (-1,0,1,-1),
                (-1,0,-1,0),
                (-1,0,-1,1),
                (-1,0,-1,-1),
                (-1,1,0,0),
                (-1,1,0,1),
                (-1,1,0,-1),
                (-1,1,1,0),
                (-1,1,1,1),
                (-1,1,1,-1),
                (-1,1,-1,0),
                (-1,1,-1,1),
                (-1,1,-1,-1),
                (-1,-1,0,0),
                (-1,-1,0,1),
                (-1,-1,0,-1),
                (-1,-1,1,0),
                (-1,-1,1,1),
                (-1,-1,1,-1),
                (-1,-1,-1,0),
                (-1,-1,-1,1),
                (-1,-1,-1,-1)
            };

            int activeNeighbours = 0;
            foreach (var (dX, dY, dZ, dW) in directions) {
                if (GetCharacterAtCoordinates(growingSuperCube, x + dX, y + dY, z + dZ, w + dW) == '#') {
                    activeNeighbours++;
                }
            }

            return activeNeighbours;
        }

        static char GetCharacterAtCoordinates(char[, ,] growingCube, int x, int y, int z) {
            if (x < 0 || x >= growingCube.GetLength(0)
                || y < 0 || y >= growingCube.GetLength(1)
                || z < 0 || z >= growingCube.GetLength(2)) {
                    return '.';
            }

            return growingCube[x, y, z];
        }

        static char GetCharacterAtCoordinates(char[, , ,] growingSuperCube, int x, int y, int z, int w) {
            if (x < 0 || x >= growingSuperCube.GetLength(0)
                || y < 0 || y >= growingSuperCube.GetLength(1)
                || z < 0 || z >= growingSuperCube.GetLength(2)
                || w < 0 || w >= growingSuperCube.GetLength(3)) {
                    return '.';
            }

            return growingSuperCube[x, y, z, w];
        }
    }
}