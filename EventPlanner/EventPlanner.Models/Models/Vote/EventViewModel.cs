using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventPlanner.Models.Domain;

namespace EventPlanner.Models.Models.Vote
{
    public class EventViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Event))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Event))]
        public string Desc { get; set; }

        [Display(Name = "Expected_length", ResourceType = typeof(Resources.Event))]
        public float ExpectedLength { get; set; }

        [Display(Name = "Organizer", ResourceType = typeof(Resources.Event))]
        public string OrganizerId { get; set; }

        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime Created { get; set; }

        [Display(Name = "Places", ResourceType = typeof(Resources.Event))]
        public IList<PlaceViewModel> Places { get; set; }

        [Display(Name = "Dates", ResourceType = typeof(Resources.Event))]
        public IList<TimeSlotViewModel> TimeSlots { get; set; }

        public int TotalNumberOfVoters { get; set; }
    }
}
