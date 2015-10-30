using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities
{
    [Table("Places")]
    public class PlaceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; }

        //[ForeignKey("EventId")]
        //public virtual EventEntity Event { set; get; }

        [Required]
        public string VenueId { set; get; }

        public ICollection<VoteForPlaceEntity> VotesForPlace { set; get; }

    }
}
