using EventManager.Areas.Admin.Models;

namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventManager.DbContexts.EventManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventManager.DbContexts.EventManagerDbContext context)
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

            context.Invitations.AddOrUpdate(i => i.Name,
                new Invitation()
                {
                    Name = "Asif Hafeez",
                    Email = "asifhafeez@softwire.com",
                    InvitationCode = "AAAAAAAAAAAA"
                },

                new Invitation()
                {
                    Name = "Felix Harrison",
                    Email = "felixharrison@softwire.com",
                    InvitationCode = "BBBBBBBBBBBB"
                },
                new Invitation()
                {
                    Name = "Jonathan Artus",
                    Email = "jonartus@softwire.com",
                    InvitationCode = "CCCCCCCCCCCC"
                }

            );
        }
    }
}
