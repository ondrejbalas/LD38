using System.Runtime.CompilerServices;
using Duality;

namespace PuddleJumper.Core.Helpers
{
    public static class MathHelpers
    {
        public static float GetDistance(this Vector2 v1, Vector2 v2)
        {
            return (v1 - v2).Length;
        }
    }
}