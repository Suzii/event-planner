using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities
{
    [Table("VotesForPlace")]
    public class VoteForPlaceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { set; get; }

        [Required]
        public Guid PlaceId { set; get; }

        [ForeignKey("PlaceId")]
        public virtual PlaceEntity Place { set; get; }
    }
}
