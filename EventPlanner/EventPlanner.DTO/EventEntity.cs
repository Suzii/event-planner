using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventPlanner.Entities
{
    [Table("Events")]
    public class EventEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Desc { get; set; }

        [Range(1, 99)]
        public double ExpectedLength { get; set; }

        [ForeignKey("Organizer")]
        public string OrganizerId { get; set; }

        public virtual UserEntity Organizer { get; set; }

        public bool OthersCanEdit { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public IList<PlaceEntity> Places { get; set; }

        public IList<TimeSlotEntity> TimeSlots { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }
    }
}
