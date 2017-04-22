using System.Collections.Generic;
using Duality;
using Duality.Resources;
using PuddleJumper.Core.GameObjects.Map;
using PuddleJumper.Core.Generators;

namespace PuddleJumper.Core.GameObjects
{
    public class World
    {
        public WorldMap MapObject { get; set; }
        public WorldMapData MapData { get; set; }
        public AirportSpawner AirportSpawner { get; set; }
        public List<AirportController> Airports { get; set; } = new List<AirportController>();

        private bool regenerateWorld;

        public World(WorldMapData mapData, AirportSpawner airportSpawner)
        {
            regenerateWorld = true;
            MapData = mapData;
            AirportSpawner = airportSpawner;
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

            AirportSpawner.Update();
        }

        public void GenerateNewMap()
        {
            MapData.Regenerate();
            MapObject.Draw(MapData);
        }
    }
}