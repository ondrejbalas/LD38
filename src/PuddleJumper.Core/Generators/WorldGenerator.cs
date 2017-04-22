using System;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Map;
using SharpNoise;
using SharpNoise.Modules;

namespace PuddleJumper.Core.Generators
{
    public class WorldGenerator
    {
        private static Random rng = new Random();
        private Perlin noise;

        public WorldGenerator()
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

        public NoiseMap Generate(int width, int height)
        {
            noise.Seed = rng.Next();
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