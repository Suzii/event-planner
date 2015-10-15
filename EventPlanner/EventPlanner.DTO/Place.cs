using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    public class Place
    {
        public Guid Id { set; get; }

        [Required]
        public Guid EventId { set; get; }

        [Required]
        public Guid VenueId { set; get; }

        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }

    }
}
