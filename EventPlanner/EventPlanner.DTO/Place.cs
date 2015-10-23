using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.DTO
{
    public class Place
    {
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; }

        [Required]
        public string VenueId { set; get; }

        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }

    }
}
