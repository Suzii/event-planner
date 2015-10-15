using System;

namespace EventPlanner.Models.Domain
{
    public class VoteForPlace
    {
        public Guid Id { set; get; }
        public Guid UserId { set; get; }
        public Guid PlaceId { set; get; }
    }
}
