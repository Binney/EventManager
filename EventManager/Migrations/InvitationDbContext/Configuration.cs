using EventManager.Areas.Admin.Models;

namespace EventManager.Migrations.InvitationDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Areas.Admin.Models.InvitationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\InvitationDbContext";
        }

        protected override void Seed(Areas.Admin.Models.InvitationDbContext context)
        {
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
