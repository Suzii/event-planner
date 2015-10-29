using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities
{
    [Table("VotesForDate")]
    public class VoteForDateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { set; get; }

        [Required]
        public Guid TimeSlotId { set; get; }

        [ForeignKey("TimeSlotId")]
        public virtual TimeSlotEntity TimeSlot { set; get; }
    }
}
