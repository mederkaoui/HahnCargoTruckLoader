﻿namespace HahnCargoTruckLoader.Model
{
    public class Truck
    {
        public required string TruckType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
    }
}