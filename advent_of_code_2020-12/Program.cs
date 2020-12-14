using System;
using System.IO;

namespace advent_of_code_2020_12
{
    class Program
    {
        private static int _rotation;
        private static (int X, int Y) _position;
        private static (int X, int Y) _wayPoint;

        static void Main(string[] args)
        {
            var directions = File.ReadAllLines("input.txt");

            Console.WriteLine(PartOne(directions));
            Console.WriteLine(PartTwo(directions));
        }

        static int PartOne(string[] directions) {
            _position = (0, 0);
            _rotation = 90;

            foreach (var direction in directions) {
                AddDirection(direction);
            }

            return Math.Abs(_position.X) + Math.Abs(_position.Y);
        }

        static int PartTwo(string[] directions) {
            _position = (0, 0);
            _wayPoint = (10, -1);

            foreach (var direction in directions) {
                AddDirectionWithWayPoint(direction);
            }

            return Math.Abs(_position.X) + Math.Abs(_position.Y);
        }

        static void AddDirectionWithWayPoint(string direction) {
            int value = int.Parse(direction[1 ..]);

            switch (direction[0]) {
                case 'N':
                    _wayPoint.Y -= value;
                    break;
                case 'S':
                    _wayPoint.Y += value;
                    break;
                case 'E':
                    _wayPoint.X += value;
                    break;
                case 'W':
                    _wayPoint.X -= value;
                    break;
                case 'L':
                    RotateWayPointClockWise(Math.Abs(value - 360));
                    break;
                case 'R':
                    RotateWayPointClockWise(value);
                    break;
                case 'F':
                    _position.X += _wayPoint.X * value;
                    _position.Y += _wayPoint.Y * value;
                    break;
            }
        }

        static void RotateWayPointClockWise(int degrees) {
            for (int i = 0; i < degrees; i += 90) {
                _wayPoint = (-_wayPoint.Y, _wayPoint.X);
            }
        }

        static void AddDirection(string direction) {
            int value = int.Parse(direction[1 ..]);

            switch (direction[0]) {
                case 'N':
                    _position.Y -= value;
                    break;
                case 'S':
                    _position.Y += value;
                    break;
                case 'E':
                    _position.X += value;
                    break;
                case 'W':
                    _position.X -= value;
                    break;
                case 'L':
                    _rotation -= value;
                    if (_rotation < 0) {
                        _rotation += 360;
                    }
                    break;
                case 'R':
                    _rotation += value;
                    if (_rotation >= 360) {
                        _rotation -= 360;
                    }
                    break;
                case 'F':
                    switch (_rotation) {
                        case 0:
                            AddDirection("N" + value);
                            break;
                        case 90:
                            AddDirection("E" + value);
                            break;
                        case 180:
                            AddDirection("S" + value);
                            break;
                        case 270:
                            AddDirection("W" + value);
                            break;
                    }
                    break;
            }
        }
    }
}
