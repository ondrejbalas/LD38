﻿using System;

namespace PuddleJumper.Core.GameObjects.Plane
{
    public class PlaneParameters
    {
        public int PassengerCapacity { get; set; }
        public float BoardingDelay { get; set; } = 3;
        public int LuxuryLevel { get; set; }
        public float Speed { get; set; }
        public float FuelBurnRate { get; set; }

        public string MaterialName { get; set; }

        public static PlaneParameters Create(PlaneTypes type)
        {
            switch (type)
            {
                case PlaneTypes.PuddleJumper:
                    return new PlaneParameters()
                    {
                        PassengerCapacity = 3,
                        BoardingDelay = 4,
                        FuelBurnRate = 1f,
                        LuxuryLevel = 0,
                        Speed = 1.0f,
                        MaterialName = "plane-small"
                    };
                case PlaneTypes.DualProp:
                    return new PlaneParameters()
                    {
                        PassengerCapacity = 6,
                        BoardingDelay = 3,
                        FuelBurnRate = 2f,
                        LuxuryLevel = 0,
                        Speed = 1.4f,
                        MaterialName = "plane-med"
                    };
                case PlaneTypes.NarrowBody:
                    return new PlaneParameters()
                    {
                        PassengerCapacity = 10,
                        BoardingDelay = 2,
                        FuelBurnRate = 8f,
                        LuxuryLevel = 0,
                        Speed = 2.4f,
                        MaterialName = "plane-med"
                    };
                case PlaneTypes.Heavy:
                    return new PlaneParameters()
                    {
                        PassengerCapacity = 24,
                        BoardingDelay = 1,
                        FuelBurnRate = 30f,
                        LuxuryLevel = 0,
                        Speed = 1.8f,
                        MaterialName = "plane-med"
                    };
                default:
                    throw new Exception("Implement this");
            }
        }
    }
}