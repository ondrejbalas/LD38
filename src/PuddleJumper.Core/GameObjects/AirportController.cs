﻿using System;
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

        public string Name { get; set; } = "Airport Name";
        public char Letter => Name[0];
        public int Size { get; set; } = 1;
        public int X => (int)GameObj.GetComponent<Transform>().Pos.X;
        public int Y => (int)GameObj.GetComponent<Transform>().Pos.Y;

        public double LastSpawnTime { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        
        private bool initComplete = false;
        public void OnInit(InitContext context)
        {
            if (context == InitContext.Activate)
            {
                GameObj.ChildByName("NameText").GetComponent<TextRenderer>().Text.SourceText = Name;
                GameObj.ChildByName("SizeText").GetComponent<TextRenderer>().Text.SourceText = Size.ToString();
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
            var timeSinceLastSpawn = Time.GameTimer.TotalSeconds - LastSpawnTime;
            if (timeSinceLastSpawn > Difficulty.Current.PassengerSpawnDelays[Size - 1])
            {
                LastSpawnTime = Time.GameTimer.TotalSeconds;
                var otherAirports = Startup.World.Airports.Where(a => a.Letter != Letter).ToList();
                Passengers.Add(new Passenger() { Destination = otherAirports[rng.Next(otherAirports.Count)].Letter });
            }

            // Update passenger list
            GameObj.ChildByName("PassengersText").GetComponent<TextRenderer>().Text.SourceText = string.Join("", Passengers.Select(p => p.ToString()));
        }
    }
}