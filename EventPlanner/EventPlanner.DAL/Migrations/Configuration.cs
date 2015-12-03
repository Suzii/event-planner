namespace EventPlanner.DAL.Migrations
{
    using System.Data.Entity.Migrations;

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
