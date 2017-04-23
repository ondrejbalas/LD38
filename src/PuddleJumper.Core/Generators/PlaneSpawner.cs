using System;
using System.Linq;
using Duality;
using Duality.Resources;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Plane;

namespace PuddleJumper.Core.Generators
{
    public class PlaneSpawner
    {
        private readonly Scorekeeper scorekeeper;
        private ContentRef<Prefab> planePrefab = null;
        private Lazy<World> lazyWorld = new Lazy<World>(() => Startup.World);
        private World world { get { return lazyWorld.Value; } }

        private Random rng = new Random();

        public PlaneSpawner(Scorekeeper scorekeeper)
        {
            this.scorekeeper = scorekeeper;
        }

        public void SpawnPlane(PlaneTypes type)
        {
            if (world.Airports.Count < 2) return;

            var airports = SelectAirports();

            if (planePrefab == null)
            {
                planePrefab = ContentProvider.RequestContent<Prefab>(@"Data\Prefabs\PlanePrefab.Prefab.res");
            }

            var obj = planePrefab.Res.Instantiate(new Vector3(airports.src.X, airports.src.Y, -100));

            var newPlane = obj.GetComponentsInChildren<PlaneController>().Single();
            newPlane.TargetAirport = airports.dest;
            newPlane.Number = GetNewPlaneNumber();
            newPlane.Type = type;
            newPlane.Scorekeeper = scorekeeper;

            world.Planes.Add(newPlane);
            Scene.Current.AddObject(obj);
        }

        private int GetNewPlaneNumber()
        {
            int num = 1;
            var usedNumbers = world.Planes.Select(x => x.Number);
            while (usedNumbers.Contains(num))
            {
                num++;
            }
            return num;
        }

        private (AirportController src, AirportController dest) SelectAirports()
        {
            int iSrc = rng.Next(world.Airports.Count);
            int iDest = rng.Next(world.Airports.Count);
            while (iDest == iSrc)
            {
                iDest = rng.Next(world.Airports.Count);
            }

            return (world.Airports[iSrc], world.Airports[iDest]);
        }
    }
}