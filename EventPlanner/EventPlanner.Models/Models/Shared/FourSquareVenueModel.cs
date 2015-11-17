using System;

namespace EventPlanner.Models.Models.Shared
{
    public class FourSquareVenueModel
    {
        public Guid Id { get; set; }

        public string VenueId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string AddressInfo { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

    }
}
