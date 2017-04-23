using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Editor;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("PuddleJumper")]
    [RequiredComponent(typeof(SpriteRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class PlaneController : Component, ICmpInitializable, ICmpUpdatable
    {
        public int Number { get; set; }
        public bool IsSelected { get; set; }
        public int Size { get; set; }

        public void OnInit(InitContext context)
        {
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            var distance = Time.TimeMult * Difficulty.Current.PlaneSpeedMultiplier;
            var transform = GameObj.GetComponent<Transform>();
            var pos = transform.Pos;
            var angle = transform.Angle;

            transform.Pos = new Vector3(new Vector2(pos.X, pos.Y) + Vector2.FromAngleLength(angle, distance), pos.Z);
        }
    }
}