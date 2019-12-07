using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    internal class Day6
    {
        static void Main(string[] args)
        {

            // It's saturday.
            // I just want to get the answer and go to the pub
            // Hence this mess. Worked first time though if that counts for anything.

            var input = File.ReadAllLines("input.txt").ToList();

            var planets = new HashSet<Planet>();

            // Get all planets in a hashset
            foreach (var line in input)
            {
                var names = line.Split(")");
                planets.Add(new Planet(names[0]));
                planets.Add(new Planet(names[1]));
            }

            // Iterate over the hashset, and look up which planets they orbit if any
            foreach (var planet in planets)
            {
                foreach (var l in input)
                {
                    var line = l.Split(")");
                    if (line[1].Equals(planet.Name))
                    {
                        planet.Orbits = planets.First(p => p.Name == line[0]);
                    }
                }
            }

            var san = planets.FirstOrDefault(p => p.Name == "SAN");
            var you = planets.FirstOrDefault(p => p.Name == "YOU");

            // Find closest common orbit to santa

            // Get a list of planets you orbit
            var pp = you.Orbits;
            var youPlanets = new List<Planet>();
            
            while (pp != null)
            {
                youPlanets.Add(pp);
                pp = pp.Orbits;
            }

            // Go down through santas planets until you find one in your list
            // That is the closest common orbit to santa
            
            pp = san.Orbits;
            var closestCommonPlanet = san.Orbits;
            while (pp != null)
            {
                if (youPlanets.Contains(pp))
                {
                    closestCommonPlanet = pp;
                    break;
                }
                pp = pp.Orbits;
            }

            // Count from santa to common planet, and you to common planet, add together and output
            var total = 0;
            pp = san.Orbits;
            while (!pp.Equals(closestCommonPlanet))
            {
                total++;
                pp = pp.Orbits;
            }

            pp = you.Orbits;
            while (!pp.Equals(closestCommonPlanet))
            {
                total++;
                pp = pp.Orbits;
            }

            Console.WriteLine(total);

            /* Part 1
            var orbits = 0;
            // Then go through each planet,
            // counting the amount of planets it takes to get to a planet that has no orbit
            foreach (var planet in planets)
            {
                var p = planet;
                while (p.Orbits != null)
                {
                    orbits++;
                    p = p.Orbits;
                }
            }

            // Output the total counts
            Console.WriteLine(orbits);
            */
        }

    }

    internal class Planet
    {
        public readonly string Name;
        public Planet Orbits;

        public Planet(string name)
        {
            Name = name;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            var p = (Planet)obj;
            return p.Name == Name;

        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
