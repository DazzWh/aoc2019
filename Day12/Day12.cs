using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Day12
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            var planets = File.ReadLines("input.txt")
                               .Select(ParsePlanet)
                               .ToList();

            var velocity = new List<Vector3>(planets);
            velocity = velocity.Select(x => new Vector3(0,0,0)).ToList();

            var states = new List<List<Vector3>>();

            var i = 0;
            while (true)
            {
                var gravity = GetGravity(planets);
                planets  = AddVectorLists(planets, gravity);
                planets  = AddVectorLists(planets, velocity);
                velocity = AddVectorLists(velocity, gravity);

                var state = new List<Vector3>(planets);
                state.AddRange(velocity);

                foreach (var s in states)
                {
                    if (!s.SequenceEqual(state)) continue;
                    Console.WriteLine(i);
                    return;
                }

                states.Add(state);
                i++;
            }

        }
        
        private static Vector3 ParsePlanet(string line)
        {
            var rgx = new Regex(@"[^\d|\-]");
            var p = line.Split(",");
            var x = rgx.Replace(p[0], "");
            var y = rgx.Replace(p[1], "");
            var z = rgx.Replace(p[2], "");

            return new Vector3(int.Parse(x), int.Parse(y), int.Parse(z));
        }

        private static List<Vector3> GetGravity(List<Vector3> planets)
        {
            var gravity = new List<Vector3>();
            foreach (var p1 in planets)
            {
                // Change of gravity
                var x = 0;
                var y = 0;
                var z = 0;

                foreach (var p2 in planets)
                {
                    if(p1.Equals(p2)) continue;

                    x += GetGravityShift(p1.X, p2.X);
                    y += GetGravityShift(p1.Y, p2.Y);
                    z += GetGravityShift(p1.Z, p2.Z);
                }

                gravity.Add(new Vector3(x, y, z));
            }

            return gravity;
        }

        private static int GetGravityShift(float a, float b)
        {
            if (a.Equals(b)) return 0;
            return a > b ? -1 : 1;
        }

        private static List<Vector3> AddVectorLists(IReadOnlyList<Vector3> v1, IReadOnlyList<Vector3> v2)
        {
            var l = new List<Vector3>();

            for (var i = 0; i < v1.Count; i++)
            {
                l.Add(new Vector3(
                    v1[i].X + v2[i].X,
                    v1[i].Y + v2[i].Y,
                    v1[i].Z + v2[i].Z
                ));
            }

            return l;
        }

        private static int AbsSumOfVector(Vector3 v)
        {
            return (int) (Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z));
        }
    }
}
