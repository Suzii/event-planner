using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class Place
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public Guid VenueId { set; get; }
        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }

    }
}
