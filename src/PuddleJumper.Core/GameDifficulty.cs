namespace PuddleJumper.Core
{
    public class Difficulty
    {
        public static Difficulty Current { get; set; } = Difficulty.Normal;

        private static Difficulty Normal => new Difficulty()
            {
                TimeBetweenAirportSpawns = 60,
                StartingAirports = 3,
                MaxAirports = 10
            };

        public int TimeBetweenAirportSpawns { get; set; }
        public int StartingAirports { get; set; }
        public int MaxAirports { get; set; }
    }
}