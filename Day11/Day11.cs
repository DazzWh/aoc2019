using System;
using System.Collections.Generic;
using System.Linq;
using IntCode;


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
            var board = new Dictionary<Tuple<long, long>, int>();

            // First input
            cpu.AddInput( 1 );

            // 2883 part 1
            
            /* Part 2

                 1    1111 111   11  111  1     11  1111
                 1    1    1  1 1  1 1  1 1    1  1    1
                 1    111  1  1 1    1  1 1    1      1
                 1    1    111  1    111  1    1 11  1
                 1    1    1    1  1 1    1    1  1 1
                 1111 1111 1     11  1    1111  111 1111
             */

            while (true)
            {
                cpu.Run();

                if (cpu.OutputLog.Last().Equals("Complete"))
                    break;

                // First output is what to paint
                var p = new Tuple<long, long>(robo.X, robo.Y);
                board[p] = int.Parse(cpu.OutputLog.First());

                // Second is where to turn
                cpu.Run();
                robo.Turn(int.Parse(cpu.OutputLog.Last()));

                // Input what the position under the robot is
                p = new Tuple<long, long>(robo.X, robo.Y);
                cpu.AddInput(board.ContainsKey(p) ?  board[p] :  0 );
            }

            // Print the points hit
            board.Keys.ToList().ForEach(Console.WriteLine);
            Console.Out.WriteLine($"Count: { board.Count }");

            PrintBoard(board);
        }

        private static void PrintBoard(IReadOnlyDictionary<Tuple<long, long>, int> board)
        {
            var minX = board.Min(x => x.Key.Item1);
            var minY = board.Min(y => y.Key.Item2);
            var maxX = board.Max(x => x.Key.Item1);
            var maxY = board.Max(y => y.Key.Item2);

            for (var y = maxY; y >= minY ; y--)
            {
                var line = "";
                for (var x = minX; x <= maxX; x++)
                {
                    if (board.ContainsKey(new Tuple<long, long>(x, y)) && board[new Tuple<long, long>(x, y)].Equals(1))
                    {
                        line += board[new Tuple<long, long>(x, y)];
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
                dir -= 1;
            }
            else
            {
                dir += 1;
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
