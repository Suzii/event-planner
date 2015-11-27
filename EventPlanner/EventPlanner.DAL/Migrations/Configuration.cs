using System.Data.Entity.Migrations.Infrastructure;

namespace EventPlanner.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventPlanner.DAL.EventPlannerContext>
    {
        /// <summary>
        ///     Method for automatic migration set
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
