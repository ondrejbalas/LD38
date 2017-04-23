using System;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Resources;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects.Map
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
            //Log.Editor.WriteWarning("Initing in context: " + context);
            if (context == InitContext.Activate)
            {
                var height = (int) Difficulty.Current.GameAreaSize;
                pixelData = new PixelData(height, height, ColorRgba.Black);

                var pixmap = new Pixmap(pixelData);
                texture = new Texture(pixmap);
                var technique = new DrawTechnique(BlendMode.Solid);
                var mainMat = new Material(technique, ColorRgba.White, texture);

                var renderer = GameObj.GetComponent<SpriteRenderer>();
                renderer.Rect = new Rect(0, 0, height, height);
                renderer.SharedMaterial = mainMat;

                var transform = GameObj.GetComponent<Transform>();
                transform.Pos = new Vector3(0, 0, 500);
            }
        }

        public void Draw(WorldMapData mapData)
        {
            var noiseMap = mapData.GetMapScaledForDrawing(pixelData.Width, pixelData.Height);

            for (int x = 0; x < pixelData.Width; x++)
            {
                for (int y = 0; y < pixelData.Height; y++)
                {
                    var mapPoint = noiseMap[x, y].ToMapPoint();
                    pixelData[x, y] = mapPoint.Color;
                }
            }

            texture.ReloadData();
        }

        public void OnShutdown(ShutdownContext context)
        {
        }
    }
}