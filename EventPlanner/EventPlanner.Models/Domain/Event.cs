using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class Event
    {
        public Event()
        {
            Places = new List<Place>();
            TimeSlots = new List<TimeSlot>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title", ResourceType = typeof(Resources.Event))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Event))]
        public string Desc { get; set; }

        [Range(1,99)]
        [Display(Name = "Expected_length", ResourceType = typeof(Resources.Event))]
        public double ExpectedLength { get; set; }

        [Display(Name = "Organizer", ResourceType = typeof(Resources.Event))]
        public string OrganizerId { get; set; }

        public User Organizer { get; set; }

        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Places", ResourceType = typeof(Resources.Event))]
        public IList<Place> Places { get; set; }

        [Display(Name = "Dates", ResourceType = typeof(Resources.Event))]
        public IList<TimeSlot> TimeSlots { get; set; }

        public bool Disabled { get; set; }
    }
}
