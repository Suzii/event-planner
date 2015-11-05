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

        [ForeignKey("User")]
        public string UserId { set; get; }

        public virtual UserEntity User { set; get; }
        
        [ForeignKey("Place")]
        public Guid PlaceId { set; get; }

        public virtual PlaceEntity Place { set; get; }
    }
}
