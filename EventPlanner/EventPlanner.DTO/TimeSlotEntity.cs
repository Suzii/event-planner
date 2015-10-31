using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities
{
    [Table("TimeSlots")]
    public class TimeSlotEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; }

        //[ForeignKey("EventId")]
        //public virtual EventEntity Event { set; get; }

        [Required]
        public DateTime DateTime { set; get; }

        public virtual ICollection<VoteForDateEntity> VotesForDate { set; get; }
    }
}
