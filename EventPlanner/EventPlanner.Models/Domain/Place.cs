using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class Place
    {
        public Guid Id { set; get; }

        public Guid EventId { get; set; }
        public Event Event { set; get; }

        [Required]
        public string VenueId { set; get; }

        public IList<VoteForPlace> VotesForPlace { set; get; }

    }
}
