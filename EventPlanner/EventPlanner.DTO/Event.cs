using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Desc { get; set; }

        public float ExpectedLength { get; set; }

        [Required]
        public Guid OrganizerId { get; set; }

        public bool OthersCanEdit { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public IEnumerable<Place> Places { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        [Required]
        public int Hash
        {
            get
            {
                return (Id + "-" + Created.ToLongDateString() + "-" + Created.ToLongTimeString()).GetHashCode();
            }
        }
    }
}
