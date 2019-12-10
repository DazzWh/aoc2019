using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day3;

namespace Day10
{
    internal class Day10
    {
        private static void Main( string[] args )
        {
            var input = File.ReadLines("test.txt").ToList();
            var asteroids = FindAsteroids(input);

            //asteroids.ToList().ForEach(Console.WriteLine);

            foreach (var a in asteroids)
            {
                var visible = asteroids.Where((b => !a.Equals(b)))
                                       .Select(b => Gradient(a, b)) 
                                       .Distinct()
                                       .Count();

            }

            // Gradients.
            // Find the gradient of each asteroid
            // Make it the lowest common ??
            // Count how many different lowest common gradients there are for each asteroid
            // Set that 
        }

        private static float Gradient(Point a, Point b)
        {
            if (b.Y - a.Y == 0)
            {
                return 0;
            }

            var g = (b.Y - a.Y) / (b.X - a.X);

            Console.Out.WriteLine(g);

            return g; 
        }

        private static IEnumerable<Point> FindAsteroids(IReadOnlyList<string> input)
        {
            // Reusing the point class from Day3
            var points = new List<Point>();
            
            for (var y = 0; y < input.Count; y++)
            for (var x = 0; x < input[y].Length; x++) 
                if (input[y][x].Equals('#'))
                    points.Add(new Point(x, y));

            return points;
        }
    }
}
