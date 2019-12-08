using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day8
{
    class Day8
    {
        private const int Wide = 25;
        private const int Tall = 6;

        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").Single();
            var layered = SplitIntoLayers(input, Wide, Tall);
            var image = CreateImageFromLayers(layered);
            PrintLayer(image);

            //var layer = GetLayerWithFewestZeros(layered);
            //OutputPart1(layer);
        }

        private static List<string> CreateImageFromLayers(List<List<string>> layered)
        {
            // 0 = black
            // 1 = white
            // 2 = transparent
            // So get the top non transparent number in each cell and make a layer from that.

            var image = new List<string>();

            for (var y = 0; y < Tall; y++)
            {
                var row = "";
                for (var x = 0; x < Wide; x++)
                {
                    // Go down through each layer at each point and find the first non 2 value
                    for (int z = 0; z < layered.Count; z++)
                    {
                        var pixel = layered[z][y][x];
                        if (!pixel.Equals('2'))
                        {
                            if (pixel.Equals('0')) pixel = ' '; // Change 0 to spaces for readability of the message

                            row += pixel;

                            break;
                        }
                    }
                }
                image.Add(row);
            }

            return image;
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

        private static void PrintLayer(List<string> layer)
        {
            PrintLayers(new List<List<string>> { layer });
        }
    }
}
