using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("TimeSlots")]
    public class TimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [ForeignKey("EventId")]
        public Guid EventId { set; get; }

        [Required]
        public DateTime DateTime { set; get; }

        public IEnumerable<VoteForDate> VotesForDate { set; get; }
    }
}
