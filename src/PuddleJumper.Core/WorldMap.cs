using Duality;
using Duality.Drawing;
using Duality.Resources;

namespace PuddleJumper.Core
{
    public class WorldMap
    {
        public WorldMap()
        {
            //var rng = new Perlin()
        }

        public void Draw()
        {
            int height = (int)DualityApp.TargetResolution.Y;

            var pixelData = new PixelData(height, height, ColorRgba.Black);
            var pixmap = new Pixmap(pixelData);
            var tex = new Texture(pixmap);
        }
    }
}