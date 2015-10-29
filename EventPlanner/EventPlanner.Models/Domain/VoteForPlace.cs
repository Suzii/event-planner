using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class VoteForPlace
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [Required]
        public Guid PlaceId { set; get; }
    }
}
