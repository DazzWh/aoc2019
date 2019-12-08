using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Day8
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").Single();
            var layered = SplitIntoLayers(input, 25, 6);
            var layer = GetLayerWithFewestZeros(layered);

            OutputPart1(layer);
        }

        private static void OutputPart1(IEnumerable<string> layer)
        {
            // Part 1 question.
            // On layer:
            // Number of 1 digits in layer * number of 2 digits
            var oneDigits = 0;
            var twoDigits = 0;

            foreach (var row in layer)
            {
                oneDigits += row.Count(x => x == '1');
                twoDigits += row.Count(x => x == '2');
            }

            Console.Out.WriteLine($"Part 1 Answer : {oneDigits * twoDigits}");
        }

        private static List<List<string>> SplitIntoLayers(string input, int wide, int tall)
        {
            // Layers < Row<> > Each layer holds a list of row of strings.
            var layered = new List<List<string>>();
            var totalLayers = input.Length / (wide * tall);

            for (var z = 0; z < totalLayers; z++)
            {
                var layer = new List<string>();

                for (var y = 0; y < tall; y++)
                {
                    var row = "";
                    for (var x = 0; x < wide; x++)
                    {
                        row += input[((z * (wide * tall)) + (y * wide) + x)];
                    }
                    layer.Add(row);
                }

                layered.Add(layer);
            }

            return layered;
        }

        private static List<string> GetLayerWithFewestZeros(List<List<string>> layers)
        {
            var lowest = new List<string>(layers[0]);
            var lowCount = int.MaxValue;
            foreach (var layer in layers)
            {
                var count = 0;

                foreach (var row in layer)
                {
                    count += row.Count(x => x == '0');
                }
                
                if (count < lowCount)
                {
                    lowCount = count;
                    lowest = layer;
                }
            }

            return lowest;
        }

        private static void PrintLayers(IReadOnlyList<List<string>> layers)
        {
            for (var i = 0; i < layers.Count; i++)
            {
                Console.Out.WriteLine($"Layer {i}:");
                foreach (var row in layers[i])
                {
                    Console.Out.WriteLine(row);
                }
            }
        }
    }
}
