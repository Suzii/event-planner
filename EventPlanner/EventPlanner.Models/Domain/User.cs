using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class User
    {
        public string Name { get; set; }

        public IList<VoteForDate> VotesForDates { get; set; }

        public IList<VoteForPlace> VotesForPlaces { get; set; }

        public IList<Event> Events { get; set; }
    }
}