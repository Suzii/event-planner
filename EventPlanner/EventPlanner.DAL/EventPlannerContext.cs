using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EventPlanner.DTO;

namespace EventPlanner.DAL
{
    public class EventPlannerContext : DbContext
    {
        public EventPlannerContext() : base("EventPlannerContext")
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<VoteForDate> VotesForDates { get; set; }
        public DbSet<VoteForPlace> VotesForPlaces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
