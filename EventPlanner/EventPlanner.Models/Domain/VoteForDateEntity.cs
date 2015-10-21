using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("VotesForDate")]
    public class VoteForDateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [ForeignKey("UserId")]
        public Guid UserId { set; get; }

        [ForeignKey("TimeSlotId")]
        public Guid TimeSlotId { set; get; }
    }
}
