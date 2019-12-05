using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

            // Get all points hit by wire2

            // Find which points are in both

            // Print the closest to zero point in both

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
