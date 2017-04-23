using System.Collections.Generic;
using System.Linq;

namespace PuddleJumper.Core
{
    public class Difficulty
    {
        public static Difficulty Current { get; set; } = Difficulty.Normal;

        private static Difficulty Normal => new Difficulty()
            {
                TimeBetweenAirportSpawns = 5,
                StartingAirports = 3,
                MaxAirports = 10,
                MinimumAirportDistance = 250,
                PassengerPatience = 30,
                PassengerSpawnDelays = new []{ 20, 15, 12, 9 }.ToList(),
                PassengerSpawnDelayFluctuation = 0.1f,
                PlaneSpeedMultiplier = 2.0f
            };

        public int TimeBetweenAirportSpawns { get; set; }
        public int StartingAirports { get; set; }
        public int MaxAirports { get; set; }
        public double MinimumAirportDistance { get; set; }
        public int PassengerPatience { get; set; } // time a passenger waits before they give up and take a bus (and you lose reputation?)
        public List<int> PassengerSpawnDelays { get; set; }
        public float PassengerSpawnDelayFluctuation { get; set; }
        public float PlaneSpeedMultiplier { get; set; }

        public int GameAreaSize { get; set; } = 1200;
        public bool TeaserMode { get; set; } = true;
    }
}