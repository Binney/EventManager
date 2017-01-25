namespace EventManager.Migrations.InvitationDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventManager.Models.InvitationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\InvitationDbContext";
        }

        protected override void Seed(EventManager.Models.InvitationDbContext context)
        {
        }
    }
}
