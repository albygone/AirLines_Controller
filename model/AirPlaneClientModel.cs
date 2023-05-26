using System;
using System.Collections.Generic;

namespace AlbyAirLines
{
    public class AirPlaneClientModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }
        public string Name { get; }
        public string Id { get; }
        public char Type { get; }

        public AirPlaneClientModel(double longitude, double latitude, string name, char type, string id)
        {
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            Type = type;
            Id = id;
        }
    }
}
