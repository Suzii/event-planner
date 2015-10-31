using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Models.Vote
{
    public class TimeSlotViewModel
    {
        public Guid Id { set; get; }

        public Guid EventId { set; get; }

        public DateTime DateTime { set; get; }

        public IEnumerable<VoteForDateByViewModel> VotesForDate { set; get; }
    }
}
