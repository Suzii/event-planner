using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public String Name { set; get; }

        public IEnumerable<EventEntity> Events { set; get; }

        public IEnumerable<VoteForDateEntity> VotesForDate { set; get; }

        public IEnumerable<VoteForPlaceEntity> VotesForPlace { set; get; }
    }
}
