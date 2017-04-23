using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Input;
using Duality.Resources;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.Shapes
{
    public class Button : Rectangle
    {
        public string Name { get; set; }
        public ColorRgba HoverColor { get; set; }
        public bool IsClicked { get; set; }
        private bool isHovering = false;

        public override void OnUpdate()
        {
            base.OnUpdate();

            // Calculate my rectangle in worldspace
            var renderer = GameObj.GetComponent<SpriteRenderer>();
            var positionTransform = GameObj.Parent.GetComponent<Transform>();
            var currentRectangle = new Rect(renderer.Rect.X + positionTransform.Pos.X, renderer.Rect.Y + positionTransform.Pos.Y, renderer.Rect.W, renderer.Rect.H);

            // Is the mouse hovering over it?
            var camera = Scene.Current.FindComponents<Camera>().Single();
            var mousePos = camera.GetSpaceCoord(DualityApp.Mouse.Pos);
            var currentFrameHovering = currentRectangle.Contains(new Vector2(mousePos.X, mousePos.Y));
            if (isHovering != currentFrameHovering)
            {
                isHovering = currentFrameHovering;
                //Redraw(isHovering ? HoverColor : Color);
            }

            // Is Clicked
            IsClicked = MouseButton.Left.IsClicked(currentRectangle);
        }
    }
}