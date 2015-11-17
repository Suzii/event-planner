using System;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models.Models.Home
{
    public class EventViewModel
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
        
        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime CreatedOn { get; set; }

        public string Hash
        {
            get
            {
                if (!Id.HasValue) return string.Empty;
                var htmlFriendly = Convert.ToBase64String(Id.Value.ToByteArray());
                htmlFriendly = htmlFriendly.Replace("/", "_");
                htmlFriendly = htmlFriendly.Replace("+", "-");
                return htmlFriendly.Substring(0, 22);
            }
        }
    }
}
