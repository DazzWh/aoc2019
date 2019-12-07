using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day7
{
    class Day7
    {
        static void Main(string[] args)
        {

            var largest = int.MinValue;

            foreach (var arr in GetPermutations(new[] { 0, 1, 2, 3, 4 }, 5))
            {

                var cpu = new IntCodeComputer("input.txt") { OutputToConsole = false };
                var nextInput = 0;
                foreach (var i in arr)
                {
                    cpu.AddInputs(new[]{ i, nextInput });
                    cpu.Run();
                    nextInput = cpu.OutputLog.Single();
                }

                if (nextInput > largest)
                {
                    largest = nextInput;
                }
            }

            Console.WriteLine(largest);
        }

        // Thank you stack overflow
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }


}
