using System.Collections.Generic;

namespace PuddleJumper.Core.GameObjects
{
    public class Scorekeeper
    {
        private List<Passenger> deplanedPassengers = new List<Passenger>();
        private List<Passenger> angryPassengers = new List<Passenger>();

        public void AddDeplanedPassenger(Passenger passenger)
        {
            deplanedPassengers.Add(passenger);
        }

        public void AddAngryPassenger(Passenger passenger)
        {
            angryPassengers.Add(passenger);
        }
    }
}