using System;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        internal static void Main( string[] args )
        {

            var total = 0;

            for (var i = 272091; i < 815432; i++)
            {
                var str = i.ToString();

                // left to right, digits never decease
                // at least 2 adjacent digits are the same
                if (!str.Zip(str.Skip(1), (a, b) => (a <= b)).All(x => x) ||
                    !str.Zip(str.Skip(1), (a, b) => (a == b)).Any(x => x))
                    continue;

                // It's friday morning after an xmas party, I'm going to write this horribly.
                var num = -1; // Current number
                var cons = 0; // Amount of current numbers that have been consecutively placed so far
                var fine = false;
                foreach (var c in str)
                {
                    var value = int.Parse(c.ToString());

                    if (value == num)
                    {
                        cons++;
                    }
                    else
                    {
                        if (cons == 1)
                            fine = true;

                        cons = 0;
                        num = value;
                    }
                }

                if (cons == 1)
                    fine = true;

                // If above is fine, increment total
                if (fine)
                {
                    total++;
                    Console.WriteLine(str);
                }
                
            }

            Console.WriteLine(total);
        }
    }
}
