using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public String Name { set; get; }

        public IEnumerable<Event> Events { set; get; }

        public IEnumerable<VoteForDate> VotesForDate { set; get; }

        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }
    }
}
