using System;
using System.Collections.Generic;
using EventPlanner.Models.Domain;

namespace EventPlanner.Models.Models.Vote
{
    public class TimeSlotViewModel
    {
        public Guid Id { set; get; }

        public Guid EventId { set; get; }

        public DateTime DateTime { set; get; }

        public IEnumerable<VoteForDate> VotesForDate { set; get; }
    }
}
