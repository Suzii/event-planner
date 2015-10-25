using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Models.Vote
{
    public class VoteForDateByViewModel
    {
        public Guid UserId { set; get; }

        public string UserName { get; set; }

        public Guid TimeSlotId { set; get; }

        public bool WillAttend { get; set; }
    }
}
