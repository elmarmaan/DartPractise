using Domain;

namespace DartPractise.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DartPractise.Contexts.DartPractiseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DartPractise.Contexts.DartPractiseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.GameTypes.AddOrUpdate(
              g => g.Name,
              new GameType
              {
                  Name = "170"
              },
              new GameType
              {
                  Name = "Around the world"
              }
            );
            
        }
    }
}
