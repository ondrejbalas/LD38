using System.Collections.Generic;
using Duality;
using Duality.Resources;

namespace PuddleJumper.Core.GameObjects
{
    public class World
    {
        public WorldMap MapObject { get; set; }
        public WorldMapData MapData { get; set; }
        public WorldGenerator MapGenerator { get; set; }
        public List<Airport> Airports { get; set; } = new List<Airport>();

        private bool regenerateWorld;

        public World(WorldMapData mapData, WorldGenerator mapGenerator)
        {
            regenerateWorld = true;
            MapData = mapData;
            MapGenerator = mapGenerator;
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
        }

        public void GenerateNewMap()
        {
            int height = (int)DualityApp.TargetResolution.Y;
            
            MapData = MapGenerator.Generate(height, height);

            MapObject.Draw(MapData);
        }
    }
}