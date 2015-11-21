using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Models.Vote
{
    /// <summary>
    /// This class serves as ViewModel for both Place and TimeSlot options
    /// that is serialized to JSON object and passed to JavaScript component.
    /// 
    /// It contains UsersVote model and VotesViewModel.
    /// </summary>
    public class OptionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Desc { get; set; }

        public UsersVoteModel UsersVote { get; set; }

        public VotesViewModel Votes { get; set; }
    }

    public class UsersVoteModel
    {
        public Guid Id { get; set; }

        public string WillAttend { get; set; }
    }

    public class VotesViewModel
    {
        public IList<string> Yes { get; set; }
        public IList<string> Maybe { get; set; }
        public IList<string> No { get; set; }
    }
}
