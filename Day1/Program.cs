using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        public static void Main( string[] args )
        {
            var list = File.ReadAllLines("input1.txt")
                            .Select(double.Parse)
                            .ToList();

            var total = 0.0;

            foreach (var ele in list)
            {
                var e = ele;
                while (true)
                {
                    var additional = Math.Floor(e / 3) - 2;
                    if (additional > 0)
                    {
                        total += additional;
                        e = additional;
                    }
                    else
                    {
                        break;
                    }
                }
                
            }

            Console.WriteLine(total);
            
        }
    }
}
