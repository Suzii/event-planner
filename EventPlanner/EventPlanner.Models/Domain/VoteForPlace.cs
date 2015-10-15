using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("VotesForPlace")]
    public class VoteForPlace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [ForeignKey("UserId")]
        public Guid UserId { set; get; }

        [ForeignKey("UserId")]
        public Guid PlaceId { set; get; }
    }
}
