using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.DTO
{
    public class VoteForPlace
    {
        public Guid Id { set; get; }

        [Required]
        public Guid UserId { set; get; }

        [Required]
        public Guid PlaceId { set; get; }
    }
}
