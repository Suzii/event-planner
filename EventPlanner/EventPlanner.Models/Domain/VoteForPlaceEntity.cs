using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("VotesForPlace")]
    public class VoteForPlaceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]//[ForeignKey("UserId")]
        public Guid UserId { set; get; }

        [Required]//[ForeignKey("UserId")]
        public Guid PlaceId { set; get; }
    }
}
