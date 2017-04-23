using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Input;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("PuddleJumper")]
    public class AirportController : Component, ICmpInitializable, ICmpUpdatable
    {
        public Scorekeeper Scorekeeper { get; set; }
        private static Random rng = new Random();

        public string Name { get; set; } = "Airport Name";
        public char Letter => string.IsNullOrEmpty(Name) ? ' ' : Name[0];
        public int Size { get; set; } = 1;
        public int X => (int)GameObj.GetComponent<Transform>().Pos.X;
        public int Y => (int)GameObj.GetComponent<Transform>().Pos.Y;

        public double NextSpawnTime { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public List<PlaneController> Planes { get; set; } = new List<PlaneController>();

        private bool initComplete = false;
        public void OnInit(InitContext context)
        {
            if (context == InitContext.Activate)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = "City Name";
                }

                GameObj.ChildByName("NameText").GetComponent<TextRenderer>().Text.SourceText = $"/cffff00fd/f[1]{Letter}/f[0]/cffffffff{Name.Substring(1)}";
                //GameObj.ChildByName("NameText").GetComponent<TextRenderer>().Text.SourceText = $"/f[1]/cffff00ff{Letter}/f[0]/cffffffff{Name.Substring(1)}";
                //GameObj.ChildByName("NameText").GetComponent<TextRenderer>().Text.SourceText = $"/cffff00ff{Letter}/cffffffff{Name.Substring(1)}";
                GameObj.ChildByName("SizeText").GetComponent<TextRenderer>().Text.SourceText = Size.ToString();
                GameObj.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = "";
                //UpdatePosition(true);
            }

            initComplete = true;
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            if (Size < 1) return;

            // Update size textbox
            GameObj.ChildByName("SizeText").GetComponent<TextRenderer>().Text.SourceText = Size.ToString();

            // Spawn passengers
            if (NextSpawnTime < Time.GameTimer.TotalSeconds)
            {
                double nextSpawnIn = Difficulty.Current.PassengerSpawnDelays[Size - 1];
                nextSpawnIn += ((rng.NextDouble() - 0.5d) * (2 * Difficulty.Current.PassengerSpawnDelayFluctuation));
                NextSpawnTime = Time.GameTimer.TotalSeconds + nextSpawnIn;

                var otherAirports = Startup.World.Airports.Where(a => a.Letter != Letter).ToList();
                Passengers.Add(new Passenger() { Destination = otherAirports[rng.Next(otherAirports.Count)].Letter });
            }

            // Update passenger list
            GameObj.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = string.Join("", Passengers.Select(p => p.ToString()));

            // Check for passengers that have run out of patience
            var angryPassengers = Passengers.Where(p => (Time.GameTimer.TotalSeconds - p.SpawnTime) > Difficulty.Current.PassengerPatience).ToList();
            foreach (var angryPassenger in angryPassengers)
            {
                Scorekeeper.AddAngryPassenger(angryPassenger);
                Passengers.Remove(angryPassenger);
            }

            var positionTransform = GameObj.GetComponent<Transform>();
            var topRect = GameObj.ChildByName("BgTop").GetComponent<SpriteRenderer>().Rect;
            var bottomRect = GameObj.ChildByName("Bg").GetComponent<SpriteRenderer>().Rect;

            var rect = new Rect(bottomRect.X, topRect.Y, topRect.W, topRect.Y + topRect.H + bottomRect.H);
            var currentRectangle = new Rect(rect.X + positionTransform.Pos.X, rect.Y + positionTransform.Pos.Y, rect.W, rect.H);

            if (DualityApp.Mouse.ButtonHit(MouseButton.Right))
            {
                ;
            }

            if (Startup.World.SelectedPlane != null)
            {
                var offset = Key.F9;
                bool isClicked = DualityApp.Keyboard.KeyHit(offset + Letter) || MouseButton.Right.IsClicked(currentRectangle);
                if (isClicked)
                {
                    Startup.World.SelectedPlane.TargetAirport = this;
                }
            }
        }
    }
}