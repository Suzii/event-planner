using EventPlanner.Models.Domain;
using System.Data.Entity;


namespace EventPlanner.DAL
{
    public class EventPlannerContext : DbContext
    {
        public EventPlannerContext() : base("EventPlannerContext")
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
    }
}
