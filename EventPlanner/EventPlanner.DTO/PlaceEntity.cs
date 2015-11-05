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

        [ForeignKey("Event")]
        public Guid EventId { get; set; }
        public virtual EventEntity Event { set; get; }

        [Required]
        public string VenueId { set; get; }

        public IList<VoteForPlaceEntity> VotesForPlace { set; get; }

    }
}
