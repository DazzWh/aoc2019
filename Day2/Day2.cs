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

            for (int i = 0; i < input.Count - 3; i+=4)
            {
                var op = input[i];
                if (op == 99)
                {
                    return;
                }

                if (op == 1)
                {
                    // Add and put in 3rd
                }

                if (op == 2)
                {
                    // Multiply and put in 3rd
                }

            }
            
            Console.WriteLine(input);
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
