using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("Places")]
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [ForeignKey("EventId")]
        public Guid EventId { set; get; }

        [ForeignKey("EventId")]
        public Guid VenueId { set; get; }

        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }

    }
}
