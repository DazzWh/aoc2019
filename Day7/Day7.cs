using System;
using System.Collections.Generic;
using System.Linq;
using IntCode;

namespace Day7
{
    class Day7
    {
        static void Main(string[] args)
        {
            var largest = int.MinValue;
            const string filename = "input.txt";

            // Should output with input.txt 1336480;

            foreach (var seq in GetPermutations(new[] { 5, 6, 7, 8, 9 }, 5))
            {
                var arr = seq.Select(x => (long)x).ToArray();

                var amps = new[]
                {
                    new Computer(filename) { OutputToConsole = false },
                    new Computer(filename) { OutputToConsole = false },
                    new Computer(filename) { OutputToConsole = false },
                    new Computer(filename) { OutputToConsole = false },
                    new Computer(filename) { OutputToConsole = false },
                };

                for (var i = 0; i < amps.Length; i++)
                {
                    amps[i].AddInputs(new []{ arr[i] });
                    amps[i].Run(); // Run once with their amp number
                }
                
                // Run amp A once with the input of 0
                amps[0].AddInputs(new long[]{ 0 });
                amps[0].Run();

                // Put the output of the first run into amp B
                amps[1].AddInputs(new long[] { int.Parse(amps[0].OutputLog.Single()) });

                var output = 0;
                var ampIdx = 1;
                
                // Generate the rest of the inputs
                while (true)
                {
                    var amp = amps[ampIdx];

                    amp.Run();
                    
                    if (ampIdx == arr.Length - 1 && amp.OutputLog.Count == 0)
                    {
                        if (largest < output)
                        {
                            largest = output;
                        }
                        break;
                    }

                    if (amp.OutputLog.Count == 0)
                    {
                        ampIdx++;
                        ampIdx %= arr.Length;
                        continue;
                    }

                    var nextInput = int.Parse(amp.OutputLog.Single());
                    
                    if (ampIdx == arr.Length - 1)
                    {
                        output = nextInput;
                    }

                    ampIdx++;
                    ampIdx %= arr.Length;
                    
                    amps[ampIdx].AddInputs(new long[]{ nextInput });
                    
                }
            }

            Console.Out.WriteLine(largest);
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
