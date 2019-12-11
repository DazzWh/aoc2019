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
            // 4563 is too high, not 4562

            var cpu = new Computer("input.txt")
            {
                OutputToConsole = false,
                StopAtOutput = true
            };

            var robo = new Robot();
            var board = new Dictionary<Point, long>();

            cpu.AddInputs(new long[] { 0 });
            cpu.Run();

            while (true)
            {
                // First output is what to paint
                var p = new Point(robo.X, robo.Y);
                board[p] = long.Parse(cpu.OutputLog.Single());

                cpu.Run();
                // Second is where to turn
                robo.Turn(cpu.OutputLog.Last());

                // Input what the position under the robot is
                p = new Point(robo.X, robo.Y);
                board.TryGetValue(p, out var v);
                cpu.AddInputs(new[] { v });

                cpu.Run();
                if (cpu.OutputLog.Last().Equals("Complete"))
                    break;
            }

            // board.Keys.ToList().ForEach(Console.WriteLine);

            Console.Out.WriteLine($"Count: { board.Count }");
        }
    }

    internal enum Direction
    {
        Left = 0, Up = 1, Right = 2, Down = 3
    }

    internal class Robot
    {
        public Direction dir;
        public int X;
        public int Y;
        

        public Robot()
        {
            dir = Direction.Up;
            X = Y = 0;
        }
        
        public void Turn(string d)
        {
            if (d.Equals("0"))
            {
                dir += -1;
            }
            else
            {
                dir += 1;
            }

            if ((int) dir == -1)
                dir = Direction.Down;

            if ((int) dir == 4)
                dir = Direction.Left;

            // Move forward 1 space
            switch (dir)
            {
                case Direction.Up:    Y += 1; break;
                case Direction.Down:  Y -= 1; break;
                case Direction.Left:  X -= 1; break;
                case Direction.Right: X += 1; break;

                default:
                    throw new ArgumentException("Invalid Direction");
            }
        }
    }
}
