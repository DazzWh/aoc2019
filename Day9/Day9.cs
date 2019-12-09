using IntCode;

namespace Day9
{
    internal class Day9
    {
        private static void Main( string[] args )
        {
            var cpu = new Computer("input.txt");
            cpu.AddInputs(new long[]{ 2 });
            cpu.Run();

            // Part 1: 4234906522
            // Part 2: 60962


        }
    }
}
