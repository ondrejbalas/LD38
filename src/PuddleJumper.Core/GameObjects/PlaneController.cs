using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Editor;
using Duality.Resources;
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
                typeChanged = true;
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
        private bool typeChanged = false;

        public void OnInit(InitContext context)
        {
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            if (typeChanged)
            {
                typeChanged = false;
                var material = ContentProvider.RequestContent<Material>($@"Data\Art\{Parameters.MaterialName}.Material.res").Res;
                var texture = material.MainTexture.Res;
                var renderer = GameObj.GetComponent<SpriteRenderer>();
                renderer.Rect = new Rect(-texture.Size.X / 2, -texture.Size.Y / 2, texture.Size.X, texture.Size.Y);
                renderer.SharedMaterial = material;

                // Pax Transform Positions are (relative) both -n where n is the greater of the sprite renderer's rect width/2 or height/2
                var paxTextTransform = GameObj.Parent.ChildByName("PassengersText").GetComponent<Transform>();
                var n = Math.Max(renderer.Rect.W / 2, renderer.Rect.H / 2);
                paxTextTransform.RelativePos = new Vector3(-n, -n, 0);
            }

            // Update passenger list
            GameObj.Parent.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = string.Join("", Passengers.Select(p => p.ToString()));

            if (!AtAirport)
            { // we're flyyyyying
                if (TargetAirport == null) return;

                var distance = Time.TimeMult * Difficulty.Current.PlaneSpeedMultiplier;
                var rotationTransform = GameObj.GetComponent<Transform>();
                var positionTransform = GameObj.Parent.GetComponent<Transform>();
                var pos = positionTransform.Pos;

                if (new Vector2(pos.X, pos.Y).GetDistance(new Vector2(TargetAirport.X, TargetAirport.Y)) > 5f)
                {
                    // Calculate new angle
                    var planeAngle = (new Vector2(TargetAirport.X, TargetAirport.Y) - new Vector2(pos.X, pos.Y)).Angle;
                    rotationTransform.Angle = planeAngle;

                    // Calculate new movement
                    positionTransform.Pos = new Vector3(new Vector2(pos.X, pos.Y) + Vector2.FromAngleLength(planeAngle, distance),
                        pos.Z);
                }
                else
                {
                    AtAirport = true;
                    TargetAirport.Planes.Add(this);
                    rotationTransform.Angle = 0;
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
                            TargetAirport = Startup.World.Airports.Single(a => a.Letter == Passengers.First().Destination);
                        }
                        else
                        {
                            TargetAirport = Startup.World.Airports[rng.Next(Startup.World.Airports.Count)];
                        }
                    }
                }
            }
        }
    }
}