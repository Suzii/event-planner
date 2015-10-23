using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("TimeSlots")]
    public class TimeSlotEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        //[ForeignKey("EventId")]
        public Guid EventId { set; get; }

        [Required]
        public DateTime DateTime { set; get; }

        public IEnumerable<VoteForDateEntity> VotesForDate { set; get; }
    }
}
