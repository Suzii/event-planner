using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class User
    {
        public IList<VoteForDate> VotesForDates { get; set; }

        public IList<VoteForPlace> VotesForPlaces { get; set; }

        public IList<Event> Events { get; set; }
    }
}