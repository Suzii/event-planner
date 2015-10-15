using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class User
    {
        public Guid Id { set; get; }
        public String Name { set; get; }
        public IEnumerable<Event> Events { set; get; }
        public IEnumerable<VoteForDate> VotesForDate { set; get; }
        public IEnumerable<VoteForPlace> VotesForPlace { set; get; }
    }
}
