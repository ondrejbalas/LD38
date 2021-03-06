﻿using System;
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

            // Find camera focus distance, 
            var cameraDistance = 500 * Math.Min(DualityApp.TargetResolution.X / (Difficulty.Current.GameAreaSize + MagicStrings.MenuWidth), DualityApp.TargetResolution.Y / Difficulty.Current.GameAreaSize);

            // Center camera
            var camTransform = camera.GameObj.GetComponent<Transform>();
            camTransform.Pos = new Vector3((Difficulty.Current.GameAreaSize + MagicStrings.MenuWidth) / 2, Difficulty.Current.GameAreaSize / 2, -500);

            if (Math.Abs(camera.FocusDist - cameraDistance) > 0.01)
            {
                camera.FocusDist = cameraDistance;
            }
        }
    }
}