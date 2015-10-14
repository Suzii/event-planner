using System;

namespace EventPlanner.Models.Domain
{
    public class TimeSlot
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public DateTime DateTime { set; get; }
    }
}
