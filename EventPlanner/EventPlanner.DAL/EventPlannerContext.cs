using EventPlanner.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace EventPlanner.DAL
{
    /// <summary>
    ///     Class that sets new event planner database context
    /// </summary>
    public class EventPlannerContext : IdentityDbContext<UserEntity>
    {
        /// <summary>
        ///     Constructor for given connection string that disables lazy loading and proxy creation
        /// </summary>
        public EventPlannerContext() : base("name=SqlConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Set of Events
        /// </summary>
        public DbSet<EventEntity> Events { get; set; }

        /// <summary>
        /// Set of Places
        /// </summary>
        public DbSet<PlaceEntity> Places { get; set; }

        /// <summary>
        /// Set of TimeSlots
        /// </summary>
        public DbSet<TimeSlotEntity> TimeSlots { get; set; }

        /// <summary>
        /// Set of VotesForDates
        /// </summary>
        public DbSet<VoteForDateEntity> VotesForDates { get; set; }

        /// <summary>
        /// Set of VotesForPlaces
        /// </summary>
        public DbSet<VoteForPlaceEntity> VotesForPlaces { get; set; }

        /// <summary>
        ///     Getter for new event planner context
        /// </summary>
        /// <returns>New instance of event planner context</returns>
        public static EventPlannerContext Get()
        {
            return new EventPlannerContext();
        }


        /// <summary>
        ///     Overriden method for renaming basic user table names
        /// </summary>
        /// <param name="modelBuilder">Gets basic model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsersClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsersLogins");
        }
    }
}
