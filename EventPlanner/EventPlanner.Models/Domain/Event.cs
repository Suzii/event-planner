using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title", ResourceType = typeof(Resources.Event))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Event))]
        public string Desc { get; set; }

        [Range(1,99)]
        [Display(Name = "Expected_length", ResourceType = typeof(Resources.Event))]
        public float ExpectedLength { get; set; }

        [Display(Name = "Organizer", ResourceType = typeof(Resources.Event))]
        public String OrganizerId { get; set; }

        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime Created { get; set; }

        [Display(Name = "Places", ResourceType = typeof(Resources.Event))]
        public IEnumerable<Place> Places { get; set; }

        [Display(Name = "Dates", ResourceType = typeof(Resources.Event))]
        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        public string Hash
        {
            get
            {
                return Convert.ToBase64String(Id.ToByteArray());
            }
        }
    }
}
