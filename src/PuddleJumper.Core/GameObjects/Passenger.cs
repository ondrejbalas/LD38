using Duality;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core.GameObjects
{
    public class Passenger
    {
        public char SpawnAirport { get; set; }
        public double SpawnTime { get; set; } = Time.GameTimer.TotalSeconds;
        public double ArrivalTime { get; set; }
        public char Destination { get; set; }

        public override string ToString()
        {
            // G 255
            // R 0-255
            // G 255-0
            // R 255-128

            var waitingTime = Time.GameTimer.TotalSeconds - SpawnTime;
            float patience = (float) (1.0f - waitingTime / Difficulty.Current.PassengerPatience);

            return $"{MathHelpers.GetColorString(patience)}{Destination}";
        }
    }
}