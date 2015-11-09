using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventPlanner.Models.Domain;

namespace EventPlanner.Models.Models.Vote
{
    public class VoteModel
    {
        public EventViewModel EventViewModel { get; set; }

        [Required]
        public IList<VoteForDate> VotesForDates { get; set; }

        [Required]
        public IList<VoteForPlace> VotesForPlaces { get; set; }
    }
}
