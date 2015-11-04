using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class VoteForDate
    {
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [Required]
        public Guid TimeSlotId { set; get; }
    }
}
