using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata;
using IntCode;

namespace Day13
{
    internal class Day13
    {
        private static void Main()
        {
            var cpu = new Computer("input.txt")
            {
                OutputToConsole = false,
                StopAtOutput = true
            };

            var blocks = new List<(int x, int y, int id)>();
            
            cpu.Run();

            while (!cpu.WantsInput)
            {
                var x  = cpu.LastOutput;
                cpu.Run();
                var y  = cpu.LastOutput;
                cpu.Run();
                var id = cpu.LastOutput;
                cpu.Run();

                blocks.Add((x, y, id));
            }

            while (true)
            {
                
                cpu.Run();
                Console.WriteLine(blocks.First(b => b.id == 4).y);
            }

            

        }
    }
}
