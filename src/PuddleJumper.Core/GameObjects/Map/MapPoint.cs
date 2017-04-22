using Duality.Drawing;

namespace PuddleJumper.Core.GameObjects.Map
{
    public class MapPoint
    {
        public static ColorRgba Sand { get; } = new ColorRgba(186, 126, 78);
        public static ColorRgba Grass { get; } = new ColorRgba(0, 104, 32);
        public static ColorRgba Water { get; } = new ColorRgba(0, 0, 120);

        public float Val { get; }

        public MapPoint(float val)
        {
            Val = val;
            if (val < 0.7f)
            {
                Color = Grass;
                IsLand = true;
            }
            else if (val < 0.76f)
            {
                Color = Sand;
                IsSand = true;
            }
            else
            {
                Color = Water;
                IsWater = true;
            }
        }

        public ColorRgba Color { get; private set; }
        public bool IsLand { get; private set; }
        public bool IsSand { get; private set; }
        public bool IsWater { get; private set; }
    }
}