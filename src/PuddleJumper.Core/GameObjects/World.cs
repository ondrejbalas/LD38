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
        public WorldGenerator MapGenerator { get; set; }
        public AirportSpawner AirportSpawner { get; set; }
        public List<Airport> Airports { get; set; } = new List<Airport>();

        private bool regenerateWorld;

        public World(WorldMapData mapData, WorldGenerator mapGenerator, AirportSpawner airportSpawner)
        {
            regenerateWorld = true;
            MapData = mapData;
            MapGenerator = mapGenerator;
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
            int height = (int)DualityApp.TargetResolution.Y;
            
            MapData.NoiseMap = MapGenerator.Generate(height, height);

            MapObject.Draw(MapData);
        }
    }
}