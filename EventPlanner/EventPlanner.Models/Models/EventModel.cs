using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Models
{
    public class EventModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title", ResourceType = typeof(Resources.Event))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Event))]
        public string Desc { get; set; }

        [Range(1,99)]
        [Display(Name = "Expected_length", ResourceType = typeof(Resources.Event))]
        public int ExpectedLength { get; set; }

        [Display(Name = "Organizer", ResourceType = typeof(Resources.Event))]
        public Guid OrganizerId { get; set; }

        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime Created { get; set; }

        [Display(Name = "Places", ResourceType = typeof(Resources.Event))]
        public IList<FourSquareVenueModel> Places { get; set; }

        [Display(Name = "Dates", ResourceType = typeof(Resources.Event))]
        //public String Dates { get; set; }
        public IList<DatesModel> Dates { get; set; }


        public class DatesModel
        {
            public DateTime Date { get; set; }

            public IList<string> Times { get; set; }
        }

        public string Hash
        {
            get
            {
                return Id.HasValue? Convert.ToBase64String(Id.Value.ToByteArray()) : string.Empty;
            }
        }
    }
}
