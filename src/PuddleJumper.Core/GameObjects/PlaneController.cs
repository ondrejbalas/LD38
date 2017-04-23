using System;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Editor;
using PuddleJumper.Core.Helpers;

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

        public AirportController TargetAirport
        {
            get { return targetAirport; }
            set
            {
                if (targetAirport == null)
                {
                    targetAirport = value;
                    return;
                }

                if (targetAirport != value)
                {
                    targetAirport.Planes.Remove(this);
                    AtAirport = false;
                    targetAirport = value;
                }
            }
        }

        public bool AtAirport { get; set; } = false;

        private Random rng = new Random();
        private double lastHop = 0;
        private AirportController targetAirport;

        public void OnInit(InitContext context)
        {
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            if (!AtAirport)
            {
                var distance = Time.TimeMult * Difficulty.Current.PlaneSpeedMultiplier;
                var transform = GameObj.GetComponent<Transform>();
                var pos = transform.Pos;

                if (new Vector2(pos.X, pos.Y).GetDistance(new Vector2(TargetAirport.X, TargetAirport.Y)) > 5f)
                {
                    // Calculate new angle
                    var planeAngle = (new Vector2(TargetAirport.X, TargetAirport.Y) - new Vector2(pos.X, pos.Y)).Angle;
                    transform.Angle = planeAngle;

                    // Calculate new movement
                    transform.Pos = new Vector3(new Vector2(pos.X, pos.Y) + Vector2.FromAngleLength(planeAngle, distance),
                        pos.Z);
                }
                else
                {
                    AtAirport = true;
                    TargetAirport.Planes.Add(this);
                    transform.Angle = 0;
                }
            }
        }
    }
}