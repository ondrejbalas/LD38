using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Editor;
using PuddleJumper.Core.GameObjects.Plane;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("PuddleJumper")]
    [RequiredComponent(typeof(SpriteRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class PlaneController : Component, ICmpInitializable, ICmpUpdatable
    {
        public Scorekeeper Scorekeeper { get; set; }
        public int Number { get; set; }
        public bool IsSelected { get; set; }

        public PlaneTypes Type
        {
            get { return type; }
            set
            {
                type = value;
                Parameters = PlaneParameters.Create(type);
            }
        }

        public List<Passenger> Passengers { get; set; } = new List<Passenger>();

        public PlaneParameters Parameters { get; set; }

        public double LastBoardTime { get; set; }

        private AirportController targetAirport;
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
        private PlaneTypes type;

        public void OnInit(InitContext context)
        {
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            if (!AtAirport)
            { // we're flyyyyying
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
            else
            { // we've landed
                if (LastBoardTime + Parameters.BoardingDelay < Time.GameTimer.TotalSeconds)
                { // enough time has passed
                    
                    //anyone need to deplane?
                    var deplaningPassenger = Passengers.FirstOrDefault(p => p.Destination == TargetAirport.Letter);
                    if (deplaningPassenger != null)
                    {
                        Passengers.Remove(deplaningPassenger);
                        deplaningPassenger.ArrivalTime = Time.GameTimer.TotalSeconds;
                        Scorekeeper.AddDeplanedPassenger(deplaningPassenger);
                        LastBoardTime = Time.GameTimer.TotalSeconds;
                    }

                    // nope? ok lets get people on, oldest first
                    else if (TargetAirport.Passengers.Any())
                    {
                        Passengers.Add(TargetAirport.Passengers[0]);
                        TargetAirport.Passengers.RemoveAt(0);
                        LastBoardTime = Time.GameTimer.TotalSeconds;
                    }

                    else
                    { // Teaser mode - pick another airport to fly to
                        if (Passengers.Any())
                        {
                            
                        }
                        TargetAirport = Startup.World.Airports[rng.Next(Startup.World.Airports.Count)];
                    }
                }
            }
        }
    }
}