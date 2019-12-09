using IntCode;

namespace Day9
{
    internal class Day9
    {
        private static void Main( string[] args )
        {
            var cpu = new Computer("input.txt");
            cpu.AddInputs(new long[]{1});
            cpu.Run();
        }
    }
}
