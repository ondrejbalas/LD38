using System;
using System.Linq;
using Duality;
using Ninject;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Map;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.Generators
{
    public class AirportSpawner
    {
        private readonly AirportNameGenerator nameGenerator;
        private readonly WorldMapData data;

        private Random rng = new Random();
        private byte nameStartCharacter = (byte) 'A';
        public int DesiredAirports { get; set; } = Difficulty.Current.StartingAirports;
        public double LastAirportSpawnInSeconds { get; set; }

        private Lazy<World> lazyWorld = new Lazy<World>(() => Startup.World);
        private World world { get { return lazyWorld.Value; } }

        public AirportSpawner(AirportNameGenerator nameGenerator, WorldMapData data)
        {
            this.nameGenerator = nameGenerator;
            this.data = data;
        }

        private void SpawnAirport()
        {
            var loc = GetValidAirportLocation();
            world.Airports.Add(new Airport(nameGenerator.GetAirportName((char)nameStartCharacter++), loc.Item1, loc.Item2));
            LastAirportSpawnInSeconds = Time.GameTimer.TotalSeconds;
        }

        private (int x, int y, MapPoint pt) GetValidAirportLocation()
        {
            var pt = GetRandomLandPoint();
            var v = new Vector2(pt.Item1, pt.Item2);
            while (world.Airports.Any(a => new Vector2(a.X, a.Y).GetDistance(v) < 300))
            {
                pt = GetRandomLandPoint();
                v = new Vector2(pt.Item1, pt.Item2);
            }
            return pt;
        }

        private (int x, int y, MapPoint pt) GetRandomLandPoint()
        {
            var pt = GetRandomPoint();
            while (!pt.Item3.IsLand)
            {
                pt = GetRandomPoint();
            }
            return pt;
        }

        private (int x, int y, MapPoint pt) GetRandomPoint()
        {
            var x = rng.Next(data.NoiseMap.Width);
            var y = rng.Next(data.NoiseMap.Height);
            var pt = data.NoiseMap[x, y].ToMapPoint();
            return (x, y, pt);
        }

        public void Update()
        {
            if (world == null) return;
            if (data?.NoiseMap == null) return;

            if (LastAirportSpawnInSeconds + Difficulty.Current.TimeBetweenAirportSpawns < Time.GameTimer.TotalSeconds)
                DesiredAirports++;

            while (world.Airports.Count < DesiredAirports)
            {
                SpawnAirport();
            }
            ;
        }
    }
}