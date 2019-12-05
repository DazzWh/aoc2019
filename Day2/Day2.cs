using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Day2
    {
        static void Main( string[] args )
        {
            var input = File.ReadLines("input.txt").Single().Split(",").Select(int.Parse).ToList();

            // Restore stuffs
            input[1] = 12;
            input[2] = 2;
            
            for (var i = 0; i < input.Count - 3; i += 4)
            {
                switch (input[i])
                {
                    case 99:
                        Console.WriteLine(input[0]);
                        return;
                    case 1:
                        // Add and put in 3rd
                        input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                        break;
                    case 2:
                        // Multiply and put in 3rd
                        input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                        break;
                    default:
                        throw new ArgumentException($"{input[i]} is an invalid operation");
                }
            }

            
        }

        /*
         * OpCode
         * Get input
         * 99 = End
         * 1 = Adds next two numbers and stores it in the 3rd position
         * 2 = Same as above but multiply
         *
         * Move iterator 4 forward.
         * Goto get input.
         */
    }
}
