using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Input;
using Duality.Resources;
using PuddleJumper.Core.GameObjects.Plane;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("PuddleJumper")]
    [RequiredComponent(typeof(SpriteRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class PlaneController : Component, ICmpUpdatable
    {
        public Scorekeeper Scorekeeper { get; set; }
        public int Number { get; set; }
        public bool IsSelected { get; set; } = false;

        public static ColorRgba SelectedColorTint { get; } = new ColorRgba(79, 196, 255);
        public static ColorRgba UnselectedColorTint { get; } = new ColorRgba(255, 255, 255);

        public GameObject PlaneInMenu { get; set; }

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

        public void OnUpdate()
        {
            CheckType();

            if (!inMenu)
            {
                AddToMenu();
            }

            // Update passenger list
            GameObj.Parent.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = string.Join("", Passengers.Select(p => p.ToString()));

            if (!AtAirport)
            {
                MovePlane();
            }
            else
            {
                HandleLandedPlane();
            }

            HandlePlaneSelection();
        }

        private void HandlePlaneSelection()
        {
            var renderer = GameObj.GetComponent<SpriteRenderer>();
            var positionTransform = GameObj.Parent.GetComponent<Transform>();
            var currentRectangle = new Rect(renderer.Rect.X + positionTransform.Pos.X, renderer.Rect.Y + positionTransform.Pos.Y, renderer.Rect.W, renderer.Rect.H);

            bool planeIsSelected = DualityApp.Keyboard.KeyHit(Key.Number0 + Number) || MouseButton.Left.IsClicked(currentRectangle);

            if (planeIsSelected)
            {
                IsSelected = true;
                if (Startup.World.SelectedPlane != null && Startup.World.SelectedPlane != this)
                {
                    Startup.World.SelectedPlane.IsSelected = false;
                }
                Startup.World.SelectedPlane = this;
            }

            renderer.ColorTint = IsSelected ? SelectedColorTint : UnselectedColorTint;
        }

        private void HandleLandedPlane()
        {
            // check if enough time has passed since the last passenger has boarded or deplaned or disembarked
            if (LastBoardTime + Parameters.BoardingDelay < Time.GameTimer.TotalSeconds)
            {
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
                else if (Passengers.Count < Parameters.PassengerCapacity && TargetAirport.Passengers.Any())
                {
                    Passengers.Add(TargetAirport.Passengers[0]);
                    TargetAirport.Passengers.RemoveAt(0);
                    LastBoardTime = Time.GameTimer.TotalSeconds;
                }

                else if (Difficulty.Current.TeaserMode)
                {
                    DoTeaserMode();
                }
            }
        }

        private void DoTeaserMode()
        {
            // Teaser mode - pick another airport to fly to
            if (Passengers.Any())
            {
                TargetAirport = Startup.World.Airports.Single(a => a.Letter == Passengers.First().Destination);
            }
            else
            {
                TargetAirport = Startup.World.Airports[rng.Next(Startup.World.Airports.Count)];
            }
        }

        private void MovePlane()
        {
            if (TargetAirport == null) return;

            var distance = Time.TimeMult * Difficulty.Current.PlaneSpeedMultiplier * Parameters.Speed;
            var rotationTransform = GameObj.GetComponent<Transform>();
            var positionTransform = GameObj.Parent.GetComponent<Transform>();
            var pos = positionTransform.Pos;

            if (new Vector2(pos.X, pos.Y).GetDistance(new Vector2(TargetAirport.X, TargetAirport.Y)) > 5f)
            {
                // Calculate new angle
                var planeAngle = (new Vector2(TargetAirport.X, TargetAirport.Y) - new Vector2(pos.X, pos.Y)).Angle;
                rotationTransform.Angle = planeAngle;

                // Calculate new movement
                positionTransform.Pos = new Vector3(new Vector2(pos.X, pos.Y) + Vector2.FromAngleLength(planeAngle, distance), pos.Z);
            }
            else
            {
                AtAirport = true;
                TargetAirport.Planes.Add(this);
                rotationTransform.Angle = 0;
                LastBoardTime = Time.GameTimer.TotalSeconds;
            }
        }

        private void CheckType()
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
        }

        private bool inMenu = false;
        private void AddToMenu()
        {
            var material = ContentProvider.RequestContent<Material>($@"Data\Art\{Parameters.MaterialName}.Material.res").Res;
            var texture = material.MainTexture.Res;
            var renderer = PlaneInMenu.ChildByName("LeftArea").ChildByName("Image").GetComponent<SpriteRenderer>();
            renderer.Rect = new Rect(-texture.Size.X / 2, -texture.Size.Y / 2, texture.Size.X, texture.Size.Y);
            renderer.SharedMaterial = material;

            PlaneInMenu.ChildByName("RightArea").ChildByName("HasPlane").Active = true;
            PlaneInMenu.ChildByName("RightArea").ChildByName("NoPlane").Active = false;
            PlaneInMenu.ChildByName("LeftArea").ChildByName("Image").Active = true;
        }

        private void RemoveFromMenu()
        {
            PlaneInMenu.ChildByName("RightArea").ChildByName("HasPlane").Active = false;
            PlaneInMenu.ChildByName("RightArea").ChildByName("NoPlane").Active = true;
            PlaneInMenu.ChildByName("LeftArea").ChildByName("Image").Active = false;
        }
    }
}