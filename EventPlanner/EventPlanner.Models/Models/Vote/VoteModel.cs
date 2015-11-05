using System.Collections.Generic;
using EventPlanner.Models.Domain;

namespace EventPlanner.Models.Models.Vote
{
    public class VoteModel
    {
        public EventViewModel EventViewModel { get; set; }

        public IList<VoteForDate> VotesForDates { get; set; }

        public IList<VoteForPlace> VotesForPlaces { get; set; }
    }
}
