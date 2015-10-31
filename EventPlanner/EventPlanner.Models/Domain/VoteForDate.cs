using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class VoteForDate
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [Required]
        public Guid TimeSlotId { set; get; }
    }
}
