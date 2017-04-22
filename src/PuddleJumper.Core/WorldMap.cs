using System;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Resources;
using SharpNoise;
using SharpNoise.Modules;

namespace PuddleJumper.Core
{
    [EditorHintCategory("GameWorld")]
    public class WorldMap : Component, ICmpInitializable
    {
        public static Random rng = new Random();
        private Perlin noise;

        public WorldMap()
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

        private NoiseMap Generate(int width, int height)
        {
            noise.Seed = rng.Next();
            var map = new NoiseMap(width, height);

            var wMult = (2d / width);
            var hMult = (2d / height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = ((float) noise.GetValue(x * wMult - 1, y * hMult - 1, 0d)) + 1f / 2;
                }
            }

            return map;
        }

        public void OnInit(InitContext context)
        {
            Log.Editor.WriteWarning("Initing in context: " + context);
            if (context == InitContext.Activate)
            {
                int height = (int)DualityApp.TargetResolution.Y;
                var map = Generate(height, height);

                var pixelData = new PixelData(height, height, ColorRgba.Black);
                FillPixelData(pixelData, map);

                var pixmap = new Pixmap(pixelData);
                var tex = new Texture(pixmap);
                var technique = new DrawTechnique(BlendMode.Solid);
                var mainMat = new Material(technique, ColorRgba.White, tex);

                var renderer = new SpriteRenderer(new Rect(0, 0, height, height), mainMat);
                var transform = new Transform();
                transform.Pos = new Vector3(-(height / 2), -(height / 2), 500);

                GameObj.RemoveComponent<SpriteRenderer>();
                GameObj.RemoveComponent<Transform>();
                GameObj.AddComponent(renderer);
                GameObj.AddComponent(transform);
            }
        }

        private void FillPixelData(PixelData pixelData, NoiseMap noiseMap)
        {
            for (int x = 0; x < noiseMap.Width; x++)
            {
                for (int y = 0; y < noiseMap.Height; y++)
                {
                    if (noiseMap[x, y] < 0.7f)
                    {
                        pixelData[x, y] = ColorRgba.Green;
                    }
                    else if (noiseMap[x, y] < 0.73f)
                    {
                        pixelData[x, y] = MagicStrings.Brown;
                    }
                    else
                    {
                        pixelData[x, y] = ColorRgba.Blue;
                    }
                }
            }
        }

        public void OnShutdown(ShutdownContext context)
        {
        }
    }
}