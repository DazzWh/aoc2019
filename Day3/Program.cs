using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;

namespace Day3
{
    internal class Program
    {
        static void Main( string[] args )
        {
            // Get input
            var input = ReadLines("input.txt").ToArray();
            var wire1 = input[0].Split(",").ToArray().Select(Instruction.ParseString);
            var wire2 = input[1].Split(",").ToArray().Select(Instruction.ParseString);

            // Get all points hit by wire1
            var points1 = PointsHitByWire(wire1);

            // Get all points hit by wire2
            var points2 = PointsHitByWire(wire2);

            // Find which points are in both
            var common = points1.Intersect(points2);

            // Print the first intersection with least steps point in both
            var closest = common
                            .Select(p => points1.FindIndex(i => i.Equals(p)) + points2.FindIndex(i => i.Equals(p)))
                            .Min();

            closest += 2; // Take into account position 0 as a step for each wire

            Console.WriteLine(closest);

        }

        static List<Point> PointsHitByWire(IEnumerable<Instruction> wire)
        {
            var position = new Point(0, 0);
            var points = new List<Point>();

            foreach (var i in wire)
            {
                switch (i.Direction)
                {
                    case 'U':
                        AddPointsInDirection(ref position, new Point(0, 1), i.Length, points);
                        break;
                    case 'D':
                        AddPointsInDirection(ref position, new Point(0, -1), i.Length, points);
                        break;
                    case 'L':
                        AddPointsInDirection(ref position, new Point(-1, 0), i.Length, points);
                        break;
                    case 'R':
                        AddPointsInDirection(ref position, new Point(1, 0), i.Length, points);
                        break;

                    default:
                        throw new Exception("Unacceptable direction");
                }
            }

            return points;
        }

        static void AddPointsInDirection(ref Point position, Point direction, int amount, List<Point> points)
        {
            for (int i = 0; i < amount; i++)
            {
                position += direction;
                points.Add(position);
            }
        }
    }

    internal class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || this.GetType() != obj.GetType()) 
            {
                return false;
            }
            
            var p = (Point) obj; 
            return (X == p.X) && (Y == p.Y);
             
        }

        public int DistanceTo(Point p)
        {
            return Math.Abs(X - p.X) + Math.Abs(Y - p.Y);
        }

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    internal class Instruction
    {
        public char Direction;
        public int Length;

        public Instruction(char direction, int length)
        {
            Direction = direction;
            Length = length;
        }

        public static Instruction ParseString(string str)
        {
            return new Instruction(str[0], int.Parse(str.TrimStart(new char[]{'U', 'D', 'L', 'R'})));
        }

        public override string ToString()
        {
            return $"{Direction}:{Length}";
        }
    }
}
