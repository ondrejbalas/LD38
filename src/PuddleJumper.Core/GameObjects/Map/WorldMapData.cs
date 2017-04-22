using System;
using SharpNoise;
using SharpNoise.Modules;

namespace PuddleJumper.Core.GameObjects.Map
{
    public class WorldMapData
    {
        public NoiseMap GameAreaMap { get; private set; }
        private NoiseMap drawMap;

        private static Random rng = new Random();
        private Perlin noise;

        public WorldMapData()
        {
            noise = new Perlin()
            {
                Seed = rng.Next(),
                Frequency = 1,
                Persistence = 0.5,
                Lacunarity = 2,
                OctaveCount = 11,
                Quality = NoiseQuality.Standard,
            };
        }

        public void Regenerate()
        {
            noise.Seed = rng.Next();
            GameAreaMap = CreateMap(Difficulty.Current.GameAreaSize, Difficulty.Current.GameAreaSize);
            drawMap = null;
        }

        public NoiseMap GetMapScaledForDrawing(int drawWidth, int drawHeight)
        {
            if (!(drawMap?.Width == drawWidth && drawMap.Height == drawHeight))
            {CreateMap(drawWidth, drawHeight);
                drawMap = CreateMap(drawWidth, drawHeight);
            } 

            return drawMap;
        }

        private NoiseMap CreateMap(int width, int height)
        {
            var map = new NoiseMap(width, height);

            var wMult = (2d / width);
            var hMult = (2d / height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = ((float)noise.GetValue(x * wMult - 1, y * hMult - 1, 0d)) + 1f / 2;
                }
            }

            return map;
        }
    }
}