using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using IntCode;
using Day3;


namespace Day11
{
    internal class Day11
    {
        internal static void Main( string[] args )
        {
            var cpu = new Computer("input.txt")
            {
                OutputToConsole = false,
                StopAtOutput = true
            };

            var robo = new Robot();
            var board = new Dictionary<Point, long>();

            // First input
            cpu.AddInputs(new long[] { 0 });

            while (true)
            {
                cpu.Run();
                if (cpu.OutputLog.Single().Equals("Complete"))
                    break;

                // First output is what to paint
                var p = new Point(robo.X, robo.Y);
                board[p] = int.Parse(cpu.OutputLog.Single());
                
                // Second is where to turn
                cpu.Run();
                robo.Turn(int.Parse(cpu.OutputLog.Single()));

                // Input what the position under the robot is
                p = new Point(robo.X, robo.Y);
                cpu.AddInputs(board.ContainsKey(p) ? new[] { board[p] } : new long[] {0});
            }

            // Print the points hit
            board.Keys.ToList().ForEach(Console.WriteLine);
            Console.Out.WriteLine($"Count: { board.Count }");

            // Print the board
            PrintBoard(board);
        }

        private static void PrintBoard(Dictionary<Point, long> board)
        {
            var minX = board.Min(x => x.Key.X);
            var minY = board.Min(y => y.Key.Y);
            var maxX = board.Max(x => x.Key.X);
            var maxY = board.Max(y => y.Key.Y);

            for (var y = minY; y < maxY ; y++)
            {
                var line = "";
                for (var x = minX; x < maxX; x++)
                {
                    if (board.ContainsKey(new Point(x, y)) && board[new Point(x, y)].Equals(1))
                    {
                        line += board[new Point(x, y)];
                    }
                    else
                    {
                        line += " ";
                    }
                }
                Console.WriteLine(line);
            }

        }
    }

    internal enum Direction
    {
        Left = 0, Up = 1, Right = 2, Down = 3
    }

    internal class Robot
    {
        private Direction dir;
        public int X;
        public int Y;
        

        public Robot()
        {
            dir = Direction.Up;
            X = Y = 0;
        }
        
        public void Turn(int d)
        {
            if (d == 0)
            {
                dir += 1;
            }
            else
            {
                dir -= 1;
            }

            if (dir < Direction.Left) dir = Direction.Down;
            if (dir > Direction.Down) dir = Direction.Left; 

            switch (dir)
            {
                case Direction.Left: X -= 1; break;
                case Direction.Up: Y += 1; break;
                case Direction.Right: X += 1; break;
                case Direction.Down: Y -= 1; break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
