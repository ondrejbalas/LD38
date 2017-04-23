using System;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Resources;

namespace PuddleJumper.Core.Shapes
{
    [EditorHintCategory("Shapes")]
    [RequiredComponent(typeof(SpriteRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class Rectangle : Component, ICmpInitializable, ICmpUpdatable
    {
        private bool colorChanged = true;
        private Rect pixelDataRectangle = new Rect();

        public ColorRgba Color
        {
            get { return color; }
            set
            {
                color = value;
                EditorUpdate();
            }
        }

        public ColorRgba BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                EditorUpdate();
            }
        }

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                EditorUpdate();
            }
        }

        private void EditorUpdate()
        {
            colorChanged = true;
            if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
            {
                OnUpdate();
            }
        }

        private PixelData pixelData;
        private Texture texture;
        private ColorRgba color;
        private ColorRgba borderColor;
        private int borderSize = 0;

        public void OnInit(InitContext context)
        {
            pixelDataRectangle = new Rect();

            if (context == InitContext.Activate)
            {
                CheckForResize();
            }
        }

        private void CheckForResize()
        {
            var renderer = GameObj.GetComponent<SpriteRenderer>();

            if (!renderer.Rect.Equals(pixelDataRectangle))
            {
                pixelDataRectangle = renderer.Rect;
                pixelData = CreatePixelData((int)renderer.Rect.W, (int)renderer.Rect.H, Color);
                var pixmap = new Pixmap(pixelData);
                texture = new Texture(pixmap);
                var technique = new DrawTechnique(BlendMode.Solid);
                var mainMat = new Material(technique, ColorRgba.White, texture);
                renderer.SharedMaterial = mainMat;
                colorChanged = false;
            }
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public virtual void OnUpdate()
        {
            CheckForResize();

            if (colorChanged)
            {
                Redraw(Color);
                colorChanged = false;
            }
        }

        protected void Redraw(ColorRgba mainColor)
        {
            var newData = CreatePixelData(pixelData.Width, pixelData.Height, mainColor);
            pixelData.SetData(newData.Data);
            texture.ReloadData();
        }

        protected PixelData CreatePixelData(int width, int height, ColorRgba mainColor)
        {
            PixelData pixelData;

            if (BorderSize > 0)
            {
                pixelData = new PixelData(width, height, BorderColor);
                var fgData = new PixelData(Math.Max(width - BorderSize * 2, 0), Math.Max(height - BorderSize * 2, 0), mainColor);
                fgData.DrawOnto(pixelData, BlendMode.Solid, BorderSize, BorderSize, Math.Max(width - BorderSize * 2, 0), Math.Max(height - BorderSize * 2, 0));
            }
            else
            {
                pixelData = new PixelData(width, height, mainColor);
            }
            return pixelData;
        }
    }
}