using System;

namespace EventPlanner.Models.Domain
{
    public class VoteForDate
    {
        public Guid Id { set; get; }
        public Guid UserId { set; get; }
        public Guid TimeSlotId { set; get; }
    }
}
