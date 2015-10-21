using System.Data.Entity.Migrations;
using EventPlanner.Models.Domain;

namespace EventPlanner.DAL.Migrations
{
    class Configuration : DbMigrationsConfiguration<EventPlannerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /*protected override void Seed(EventPlannerContext context)
        {
            context.Events.AddOrUpdate(
                new[]
                {
                    new EventEntity
                    {
                        //Attributes...
                    }
                });
        }*/

    }
}
