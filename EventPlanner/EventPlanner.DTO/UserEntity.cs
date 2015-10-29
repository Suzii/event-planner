using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Extension for generated user entity, that contains collections of joined entities
    /// </summary>
    public class UserEntity : IdentityUser
    {
        public virtual ICollection<VoteForDateEntity> VotesForDates { get; set; }

        public virtual ICollection<VoteForPlaceEntity> VotesForPlaces { get; set; }

        public virtual ICollection<EventEntity> Events { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}
