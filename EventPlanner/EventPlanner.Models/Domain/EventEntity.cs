﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models.Domain
{
    [Table("Events")]
    public class EventEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public Guid OrganizerId { get; set; }

        [Display(Name = "Others_can_edit", ResourceType = typeof(Resources.Event))]
        public bool OthersCanEdit { get; set; }

        [Display(Name = "Created_on", ResourceType = typeof(Resources.Event))]
        public DateTime Created { get; set; }

        [Display(Name = "Places", ResourceType = typeof(Resources.Event))]
        public IEnumerable<PlaceEntity> Places { get; set; }

        [Display(Name = "Dates", ResourceType = typeof(Resources.Event))]
        public IEnumerable<TimeSlotEntity> TimeSlots { get; set; }

        public int Hash
        {
            get
            {
                return (Id + "-" + Created.ToLongDateString() + "-" + Created.ToLongTimeString()).GetHashCode();
            }
        }
    }
}