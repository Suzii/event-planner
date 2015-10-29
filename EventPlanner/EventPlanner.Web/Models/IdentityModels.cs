using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using EventPlanner.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventPlanner.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext()
            : base("SqlConnectionString", throwIfV1Schema: false) // was default DefaultConnection
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

        //    modelBuilder.Entity<UserEntity>().ToTable("Users");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("UsersClaims");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("UsersLogins");
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}