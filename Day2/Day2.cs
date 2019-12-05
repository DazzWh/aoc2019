using System;
using System.IO;
using System.Linq;
using Utils;

namespace Day2
{
    internal class Day2
    {
        private static void Main( string[] args )
        {
            var input = File.ReadLines("input.txt").Single().Split(",").Select(int.Parse).ToList();

            input[1] = 12;
            input[2] = 2;
            Console.Write(Intcode.Compute(input)[0]);
        }

        
    }
}
