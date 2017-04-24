using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Components.Renderers;
using Duality.Resources;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects
{
    public class Scorekeeper
    {
        private List<Passenger> deplanedPassengers = new List<Passenger>();
        private List<Passenger> angryPassengers = new List<Passenger>();
        public int Money { get; set; } = 5000;

        private Lazy<World> lazyWorld = new Lazy<World>(() => Startup.World);
        private World world { get { return lazyWorld.Value; } }

        public void AddDeplanedPassenger(Passenger passenger)
        {
            var src = world.Airports.Single(a => a.Letter == passenger.SpawnAirport);
            var dest = world.Airports.Single(a => a.Letter == passenger.Destination);

            var dist = new Vector2(src.X, src.Y).GetDistance(new Vector2(dest.X, dest.Y));
            var travelTime = passenger.ArrivalTime - passenger.SpawnTime;
            var timeMoneyMultiplier = Math.Max((Difficulty.Current.PassengerPatience - travelTime) / Difficulty.Current.PassengerPatience, 0) * Difficulty.Current.MoneyMultiplier;
            Money += (int)(timeMoneyMultiplier * dist);

            deplanedPassengers.Add(passenger);
        }

        public void AddAngryPassenger(Passenger passenger)
        {
            angryPassengers.Add(passenger);
        }

        public void UpdateUi()
        {
            Scene.Current.FindGameObject("Menu").ChildByName("MoneyText").GetComponent<TextRenderer>().Text.SourceText = $"$ {Money:#,##0}";
        }
    }
}