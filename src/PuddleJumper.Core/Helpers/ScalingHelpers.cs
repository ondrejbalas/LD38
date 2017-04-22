using System;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Resources;

namespace PuddleJumper.Core.Helpers
{
    public static class ScalingHelpers
    {
        public static void SetScale()
        {
            var camera = Scene.Current.FindComponents<Camera>().Single();

            var newDistance = 500 * Math.Min(DualityApp.TargetResolution.X / Difficulty.Current.GameAreaSize, DualityApp.TargetResolution.Y / Difficulty.Current.GameAreaSize);

            if (Math.Abs(camera.FocusDist - newDistance) > 0.01)
            {
                camera.FocusDist = newDistance;
            }
        }
    }
}