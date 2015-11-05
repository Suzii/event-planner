using System;

namespace EventPlanner.Models.Models.Vote
{
    public class VoteForPlaceByViewModel
    {
        public string UserId { set; get; }

        public string UserName { get; set; }

        public Guid VenuId { set; get; }

        public bool WillAttend { get; set; }
    }
}
