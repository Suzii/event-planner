using System.Configuration;
using EventPlanner.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace EventPlanner.DAL
{
    public class EventPlannerContext : IdentityDbContext<UserEntity>
    {
        public EventPlannerContext() : base("name=SqlConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<EventEntity> Events { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }
        public DbSet<TimeSlotEntity> TimeSlots { get; set; }
        public DbSet<VoteForDateEntity> VotesForDates { get; set; }
        public DbSet<VoteForPlaceEntity> VotesForPlaces { get; set; }

        public static EventPlannerContext Get()
        {
            return new EventPlannerContext();
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
    }
}
