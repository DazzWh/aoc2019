using System;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day1
{
    internal class Program
    {
        public static void Main( string[] args )
        {
            var list = File.ReadAllLines("input1.txt");
            foreach (var l in list)
            {
                Console.WriteLine(l);  
            }
            
        }
    }
}
