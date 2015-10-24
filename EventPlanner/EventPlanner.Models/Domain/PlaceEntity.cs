using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("Places")]
    public class PlaceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]//[ForeignKey("EventId")]
        public Guid EventId { set; get; } 

        [Required]//[ForeignKey("EventId")]
        public string VenueId { set; get; }

        public IEnumerable<VoteForPlaceEntity> VotesForPlace { set; get; }

    }
}
