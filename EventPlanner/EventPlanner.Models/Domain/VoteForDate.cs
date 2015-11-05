using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class VoteForDate
    {
        public Guid Id { set; get; }

        [Required]
        public string UserId { set; get; }

        [Required]
        public Guid TimeSlotId { set; get; }

        public bool WillAttend { get; set; }
    }
}
