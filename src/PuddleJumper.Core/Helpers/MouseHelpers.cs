using System.Linq;
using Duality;
using Duality.Components;
using Duality.Input;
using Duality.Resources;

namespace PuddleJumper.Core.Helpers
{
    public static class MouseHelpers
    {
        public static bool IsClicked(this MouseButton button, Rect hitbox)
        {
            var camera = Scene.Current.FindComponents<Camera>().Single();
            var mousePos = camera.GetSpaceCoord(DualityApp.Mouse.Pos);

            return hitbox.Contains(new Vector2(mousePos.X, mousePos.Y)) && DualityApp.Mouse.ButtonHit(button);
        }
    }
}