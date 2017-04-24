using System;
using System.Linq;
using Duality;
using Duality.Components.Renderers;
using Duality.Resources;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Plane;
using PuddleJumper.Core.Helpers;

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

        public void SpawnPlane(PlaneTypes type, int number = 0)
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
            newPlane.Number = number > 0 ? number : GetNewPlaneNumber();
            newPlane.Type = type;
            newPlane.Scorekeeper = scorekeeper;

            newPlane.PlaneInMenu = MenuHelpers.GetPlaneInMenuPrefab(newPlane.Number);

            var hasPlaneObj = newPlane.PlaneInMenu.ChildByName("RightArea").ChildByName("HasPlane");

            // Set sell text
            var textRenderer = hasPlaneObj.ChildByName("SellButton").ChildByName("Text").GetComponent<TextRenderer>();
            var amount = type.GetValue();
            textRenderer.Text.SourceText = $"Sell +${amount:###,##0}";

            // Update parameters
            hasPlaneObj.ChildByName("1Seats").ChildByName("Text").GetComponent<TextRenderer>().Text.SourceText = $"{newPlane.Parameters.PassengerCapacity} seats";
            hasPlaneObj.ChildByName("2Boarding").ChildByName("Text").GetComponent<TextRenderer>().Text.SourceText = $"{newPlane.Parameters.BoardingDelay} sec";
            hasPlaneObj.ChildByName("3Fuel").ChildByName("Text").GetComponent<TextRenderer>().Text.SourceText = $"${newPlane.Parameters.FuelBurnRate}//s";
            hasPlaneObj.ChildByName("4Speed").ChildByName("Text").GetComponent<TextRenderer>().Text.SourceText = $"{newPlane.Parameters.Speed} mph";

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