using System;
using System.Collections.Generic;
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
            
            Console.WriteLine("Brute force solution, input is between 0, 99");

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var testInput = new List<int>(input)
                    {
                        [1] = noun, [2] = verb
                    };
                    var testOutput = 0;

                    try
                    {
                        testOutput = Intcode.Compute(testInput)[0];
                        Console.WriteLine($"n {noun}, v {verb}, output {testOutput}");
                    }
                    catch (ArgumentException e)
                    {
                        //Console.WriteLine($"IncodeFail {noun} {verb}");
                    }


                    if ( testOutput == 19690720 )
                    {
                        Console.WriteLine($"noun = {noun}");
                        Console.WriteLine($"verb = {verb}");
                        Console.WriteLine($"100 * noun + verb = {(100 * noun) + verb}");
                        return;
                    }
                    

                }
            }

            Console.WriteLine("NOT FOUND");
        }

        
    }
}
