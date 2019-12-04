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
                total += Math.Floor(ele / 3) - 2;
            }

            Console.WriteLine(total);
            
        }
    }
}
