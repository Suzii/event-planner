using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Extension for generated user entity, that contains collections of joined entities
    /// </summary>
    public class UserEntity : IdentityUser
    {
        public virtual IEnumerable<VoteForDateEntity> VotesForDates { get; set; }

        public virtual IEnumerable<VoteForPlaceEntity> VotesForPlaces { get; set; }

        public virtual IEnumerable<EventEntity> Events { get; set; }
    }
}
