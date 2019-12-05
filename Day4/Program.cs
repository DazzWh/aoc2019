using System;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main( string[] args )
        {

            var total = 0;

            for (int i = 272091; i < 815432; i++)
            {
                var str = i.ToString();

                // left to right, digits never decease
                // at least 2 adjacent digits are the same
                if (str.Zip(str.Skip(1), (a, b) => (a <= b)).All(x => x) &&
                    str.Zip(str.Skip(1), (a, b) => (a == b)).Any(x => x))
                    total++;

            }

            Console.WriteLine(total);
        }
    }
}
