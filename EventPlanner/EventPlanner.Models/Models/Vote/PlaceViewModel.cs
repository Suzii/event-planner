using System;
using System.Collections.Generic;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models.Shared;

namespace EventPlanner.Models.Models.Vote
{
    public class PlaceViewModel
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public string VenueId { set; get; }

        public FourSquareVenueModel Venue { get; set; }

        public IList<VoteForPlace> VotesForPlace { set; get; }
    }
}
