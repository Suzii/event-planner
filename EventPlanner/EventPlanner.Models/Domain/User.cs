using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class User
    {
        public User()
        {
            VotesForDates = new List<VoteForDate>();
            VotesForPlaces = new List<VoteForPlace>();
            Events = new List<Event>();
        }

        public string Name { get; set; }

        public IList<VoteForDate> VotesForDates { get; set; }

        public IList<VoteForPlace> VotesForPlaces { get; set; }

        public IList<Event> Events { get; set; }
    }
}