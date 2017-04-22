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

        /// <summary>
        /// Returns a color string that is green at 1.0f, and dark red at 0.0f.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetColorString(float value)
        {
            var val = MathF.Clamp(value, 0.0f, 1.0f);

            float g = MathF.Clamp((val * 2.5f) - 0.5f, 0.0f, 1.0f);
            float r = MathF.Clamp(2.5f * (1.0f - (MathF.Abs(-1.0f * (val - 0.4f)))) - 1.0f, 0.0f, 1.0f);

            byte gb = (byte) (g >= 1.0f ? 255 : 256 * g);
            byte rb = (byte) (r >= 1.0f ? 255 : 256 * r);

            return $"/c{rb:X2}{gb:X2}00ff".ToLower();
        }
    }
}