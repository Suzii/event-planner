using System;

namespace EventPlanner.Models.Domain
{
    public class Place
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public Guid VenueId { set; get; }
    }
}
