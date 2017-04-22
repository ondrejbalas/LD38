using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;

namespace PuddleJumper.Core.GameObjects
{
    [EditorHintCategory("PuddleJumper")]
    public class AirportController : Component, ICmpInitializable, ICmpUpdatable
    {
        private static Random rng = new Random();
        private int x = 0;
        private int y = 0;

        public string Name { get; set; } = "Airport Name";
        public char Letter => Name[0];
        public int Size { get; set; } = 1;

        public int X
        {
            get { return x; }
            set
            {
                x = value;
                UpdatePosition();
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                UpdatePosition();
            }
        }

        public double LastSpawnTime { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();

        private bool initComplete = false;
        private void UpdatePosition(bool force = false)
        {
            if (initComplete || force)
            {
                var transform = GameObj.GetComponent<Transform>();
                transform.Pos = new Vector3(X - 600, Y - 600, 0);
            }
        }

        public void OnInit(InitContext context)
        {
            if (context == InitContext.Activate)
            {
                GameObj.ChildByName("NameText").GetComponent<TextRenderer>().Text.SourceText = Name;
                GameObj.ChildByName("SizeText").GetComponent<TextRenderer>().Text.SourceText = Size.ToString();
                UpdatePosition(true);
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
            var timeSinceLastSpawn = Time.GameTimer.TotalSeconds - LastSpawnTime;
            if (timeSinceLastSpawn > Difficulty.Current.PassengerSpawnDelays[Size - 1])
            {
                LastSpawnTime = Time.GameTimer.TotalSeconds;
                var otherAirports = Startup.World.Airports.Where(a => a.Letter != Letter).ToList();
                Passengers.Add(new Passenger() { Destination = otherAirports[rng.Next(otherAirports.Count)].Letter});
            }

            // Update passenger list
            GameObj.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = string.Join("", Passengers.Select(p => p.ToString()));
        }
    }
}