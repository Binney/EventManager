using EventManager.Areas.Admin.Models;

namespace EventManager.Migrations.EventDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Areas.Admin.Models.EventDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\EventDbContext";
        }

        protected override void Seed(Areas.Admin.Models.EventDbContext context)
        {
          context.Events.AddOrUpdate(i => i.Name,
              new Event
              {
                  Name = "Ping Pong Tournament",
                  Date = DateTime.Parse("01/02/2017"),
                  Time = TimeSpan.Parse("18:00")
              },

            new Event
            {
                Name = "Dinner",
                Date = DateTime.Parse("28/01/2017"),
                Time = TimeSpan.Parse("20:00")
            },

            new Event
            {
                Name = "Rock Climbing",
                Date = DateTime.Parse("23/02/2017"),
                Time = TimeSpan.Parse("17:00")
            }
          );
        }
    }
}
