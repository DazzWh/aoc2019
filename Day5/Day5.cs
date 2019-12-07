using System;
using System.IO;
using System.Linq;
using Utils;

namespace Day5
{
    class Day5
    {
        static void Main( string[] args )
        {
            // 3rd instruction is created by the first 2 instructions
            // Create your own tests for this, maybe use it to learn NUnit
            var input = File.ReadLines("input.txt").Single().Split(",").Select(int.Parse).ToList();
            var cpu = new IntCodeComputer(input, 1);
            cpu.Run();
        }
    }
}
