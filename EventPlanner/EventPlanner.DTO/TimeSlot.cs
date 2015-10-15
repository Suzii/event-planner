using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class TimeSlot
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public DateTime DateTime { set; get; }
        public IEnumerable<VoteForDate> VotesForDate { set; get; }
    }
}
