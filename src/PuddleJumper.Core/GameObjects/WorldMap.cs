using System;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Resources;
using SharpNoise;
using SharpNoise.Modules;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("GameWorld")]
    [RequiredComponent(typeof(SpriteRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class WorldMap : Component, ICmpInitializable
    {
        private PixelData pixelData;
        private Texture texture;

        public void OnInit(InitContext context)
        {
            Log.Editor.WriteWarning("Initing in context: " + context);
            if (context == InitContext.Activate)
            {
                var height = (int) DualityApp.TargetResolution.Y;
                pixelData = new PixelData(height, height, ColorRgba.Black);

                var pixmap = new Pixmap(pixelData);
                texture = new Texture(pixmap);
                var technique = new DrawTechnique(BlendMode.Solid);
                var mainMat = new Material(technique, ColorRgba.White, texture);

                var renderer = GameObj.GetComponent<SpriteRenderer>();
                renderer.Rect = new Rect(0, 0, height, height);
                renderer.SharedMaterial = mainMat;

                var transform = GameObj.GetComponent<Transform>();
                transform.Pos = new Vector3(-(height / 2), -(height / 2), 500);
            }
        }

        public void Draw(WorldMapData mapData)
        {
            if(pixelData.Height != mapData.NoiseMap.Height || pixelData.Width != mapData.NoiseMap.Width) 
                throw new ArgumentOutOfRangeException("mapData", "NoiseMap and PixelData are not the same size");

            var noiseMap = mapData.NoiseMap;

            for (int x = 0; x < pixelData.Width; x++)
            {
                for (int y = 0; y < pixelData.Height; y++)
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

            texture.ReloadData();
        }

        public void OnShutdown(ShutdownContext context)
        {
        }
    }
}