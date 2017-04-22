using Duality.Drawing;

namespace PuddleJumper.Core
{
    public static class MagicStrings
    {
        public const string MainScene = "MainScene";
        public static ColorRgba Sand { get; } = new ColorRgba(153, 82, 60);
        public static ColorRgba Grass { get; } = new ColorRgba(0, 104, 32);
        public static ColorRgba Ocean { get; } = new ColorRgba(0, 0, 120);
    }
}