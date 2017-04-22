using PuddleJumper.Core.GameObjects.Map;

namespace PuddleJumper.Core.Helpers
{
    public static class MapHelpers
    {
        public static MapPoint ToMapPoint(this float val)
        {
            return new MapPoint(val);
        }
    }
}