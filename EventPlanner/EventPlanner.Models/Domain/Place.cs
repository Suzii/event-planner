using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class Place
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; } 

        [Required]
        public string VenueId { set; get; }

        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }

    }
}
