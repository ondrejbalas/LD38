using System;
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
        public AirportController TargetAirport { get; set; }

        private Random rng = new Random();
        private double lastHop = 0;

        public void OnInit(InitContext context)
        {
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            if (lastHop + 5 < Time.GameTimer.TotalSeconds)
            {
                lastHop = Time.GameTimer.TotalSeconds;
                TargetAirport = Startup.World.Airports[rng.Next(Startup.World.Airports.Count)];
            }

            var distance = Time.TimeMult * Difficulty.Current.PlaneSpeedMultiplier;
            var transform = GameObj.GetComponent<Transform>();
            var pos = transform.Pos;

            // Calculate new angle
            var planeAngle = (new Vector2(TargetAirport.X, TargetAirport.Y) - new Vector2(pos.X, pos.Y)).Angle;
            transform.Angle = planeAngle;

            // Calculate new movement
            transform.Pos = new Vector3(new Vector2(pos.X, pos.Y) + Vector2.FromAngleLength(planeAngle, distance), pos.Z);
        }
    }
}