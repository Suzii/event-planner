using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPlanner.Entities.Enums;

namespace EventPlanner.Entities
{
    [Table("VotesForDate")]
    public class VoteForDateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [ForeignKey("User")]
        public string UserId { set; get; }

        public virtual UserEntity User { set; get; }

        [ForeignKey("TimeSlot")]
        public virtual Guid TimeSlotId { set; get; }

        public virtual TimeSlotEntity TimeSlot { set; get; }

        public WillAttend WillAttend { get; set; }
    }
}
