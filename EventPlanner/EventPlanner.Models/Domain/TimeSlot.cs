using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class TimeSlot
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; }

        [Required]
        public DateTime DateTime { set; get; }

        public IEnumerable<VoteForDate> VotesForDate { set; get; }
    }
}
