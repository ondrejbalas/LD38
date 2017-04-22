﻿using System;
using System.Linq;
using Duality;
using Duality.Resources;
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

        private ContentRef<Prefab> airportPrefab = null;

        private Random rng = new Random();
        private byte nameStartCharacter = (byte)'A';
        public int DesiredAirports { get; set; } = Difficulty.Current.StartingAirports;
        private bool spawnAirports = true;
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
            if (loc == null) return;

            if (airportPrefab == null)
            {
                airportPrefab = ContentProvider.RequestContent<Prefab>(@"Data\Prefabs\AirportPrefab.Prefab.res");
            }
            
            var obj = airportPrefab.Res.Instantiate(new Vector3(loc.Value.Item1, loc.Value.Item2, 0));
            var newAirport = obj.GetComponent<AirportController>();
            newAirport.Name = nameGenerator.GetAirportName((char) nameStartCharacter++);

            world.Airports.Add(newAirport);
            LastAirportSpawnInSeconds = Time.GameTimer.TotalSeconds;
        }

        private (int x, int y, MapPoint pt)? GetValidAirportLocation()
        {
            int failCounter = 0;

            var pt = GetRandomLandPoint();
            var v = new Vector2(pt.Item1, pt.Item2);
            while (world.Airports.Any(a => new Vector2(a.X, a.Y).GetDistance(v) < Difficulty.Current.MinimumAirportDistance))
            {
                failCounter++;

                if (failCounter > 100)
                {
                    spawnAirports = false;
                    return null;
                }

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
            var x = rng.Next(data.GameAreaMap.Width);
            var y = rng.Next(data.GameAreaMap.Height);
            var pt = data.GameAreaMap[x, y].ToMapPoint();
            return (x, y, pt);
        }

        public void Update()
        {
            if (world == null) return;
            if (data?.GameAreaMap == null) return;

            if (LastAirportSpawnInSeconds + Difficulty.Current.TimeBetweenAirportSpawns < Time.GameTimer.TotalSeconds)
                DesiredAirports++;

            while (spawnAirports && world.Airports.Count < DesiredAirports)
            {
                SpawnAirport();
            }
            ;
        }
    }
}