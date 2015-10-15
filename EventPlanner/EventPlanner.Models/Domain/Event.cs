using System;
using System.Collections.Generic;

namespace EventPlanner.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public float ExpectedLength { get; set; }
        public Guid OrganizerId { get; set; }
        public bool OthersCanEdit { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Place> Places { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }
        public int Hash
        {
            get
            {
                return (Id + "-" + Created.ToLongDateString() + "-" + Created.ToLongTimeString()).GetHashCode();
            }
        }
    }
}
