using System.Collections.Generic;
using System.Linq;

namespace PuddleJumper.Core
{
    public class Difficulty
    {
        public static Difficulty Current { get; set; } = Difficulty.Normal;

        private static Difficulty Test => new Difficulty()
            {
                TimeBetweenAirportSpawns = 60,
                StartingAirports = 6,
                MaxAirports = 7,
                MinimumAirportDistance = 250,
                PassengerPatience = 60,
                PassengerSpawnDelays = new []{ 16, 11, 9, 5 }.ToList(),
                PassengerSpawnDelayFluctuation = 0.2f,
                PlaneSpeedMultiplier = 1.5f / 250,
                MoneyMultiplier = 25.0f,
                PassengersDeliveredToUpgradeAirport = 4,
                MaxAirportSize = 4,
                StartingMoney = 412410
            };

        private static Difficulty Normal => new Difficulty()
            {
                TimeBetweenAirportSpawns = 120,
                StartingAirports = 3,
                MaxAirports = 7,
                MinimumAirportDistance = 250,
                PassengerPatience = 60,
                PassengerSpawnDelays = new []{ 16, 11, 9, 5 }.ToList(),
                PassengerSpawnDelayFluctuation = 0.2f,
                PlaneSpeedMultiplier = 1.5f / 250,
                MoneyMultiplier = 6.0f,
                PassengersDeliveredToUpgradeAirport = 20,
                MaxAirportSize = 4,
                StartingMoney = 5000
            };


        public int TimeBetweenAirportSpawns { get; set; }
        public int StartingAirports { get; set; }
        public int MaxAirports { get; set; }
        public double MinimumAirportDistance { get; set; }
        public int PassengerPatience { get; set; } // time a passenger waits before they give up and take a bus (and you lose reputation?)
        public List<int> PassengerSpawnDelays { get; set; }
        public float PassengerSpawnDelayFluctuation { get; set; }
        public float PlaneSpeedMultiplier { get; set; }
        public float MoneyMultiplier { get; set; }
        public int PassengersDeliveredToUpgradeAirport { get; set; }
        public int MaxAirportSize { get; set; }
        public int StartingMoney { get; set; }

        public int GameAreaSize { get; set; } = 1200;
        public bool TeaserMode { get; set; } = false;
    }
}