using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class TimeSlot
    {
        public Guid Id { set; get; }

        public Guid EventId { get; set; }
        public Event Event { set; get; }

        [Required]
        public DateTime DateTime { set; get; }

        public IList<VoteForDate> VotesForDate { set; get; }
    }
}
