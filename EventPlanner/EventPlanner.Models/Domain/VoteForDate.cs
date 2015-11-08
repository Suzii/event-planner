using System;
using System.ComponentModel.DataAnnotations;
using EventPlanner.Models.Enums;

namespace EventPlanner.Models.Domain
{
    public class VoteForDate
    {
        public Guid Id { set; get; }

        [Required]
        public string UserId { set; get; }

        [Required]
        public Guid TimeSlotId { set; get; }

        [Required]
        [UIHint("WillAttend")]
        public WillAttend? WillAttend { get; set; }
    }
}
