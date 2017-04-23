using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;
using Duality.Resources;
using PuddleJumper.Core.GameObjects.Map;
using PuddleJumper.Core.GameObjects.Plane;
using PuddleJumper.Core.Generators;

namespace PuddleJumper.Core.GameObjects
{
    public class World
    {
        public WorldMap MapObject { get; set; }
        public WorldMapData MapData { get; set; }
        public AirportSpawner AirportSpawner { get; set; }
        public PlaneSpawner PlaneSpawner { get; set; }
        public List<AirportController> Airports { get; set; } = new List<AirportController>();
        public List<PlaneController> Planes { get; set; } = new List<PlaneController>();
        public PlaneController SelectedPlane { get; set; } = null;

        private bool regenerateWorld;

        public World(WorldMapData mapData, AirportSpawner airportSpawner, PlaneSpawner planeSpawner)
        {
            regenerateWorld = true;
            MapData = mapData;
            AirportSpawner = airportSpawner;
            PlaneSpawner = planeSpawner;
        }

        public void RegenerateWorld()
        {
            regenerateWorld = true;
        }

        public void Update()
        {
            if (regenerateWorld)
            {
                if(MapObject == null)
                    MapObject = Scene.Current.FindComponent<WorldMap>();

                if (MapObject != null)
                {
                    GenerateNewMap();
                    regenerateWorld = false;
                }
            }

            if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
            {
                return;
            }

            AirportSpawner.Update();
            if (!Planes.Any())
            {
                PlaneSpawner.SpawnPlane(PlaneTypes.PuddleJumper);
                //PlaneSpawner.SpawnPlane(PlaneTypes.DualProp);
                //PlaneSpawner.SpawnPlane(PlaneTypes.DualProp);
            }
        }

        public void GenerateNewMap()
        {
            MapData.Regenerate();
            MapObject.Draw(MapData);
        }
    }
}