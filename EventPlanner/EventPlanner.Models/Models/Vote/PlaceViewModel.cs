using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Models.Vote
{
    public class PlaceViewModel
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public string VenueId { set; get; }
        public IEnumerable<VoteForPlaceByViewModel> VotesForPlaceBy { set; get; }
    }
}
