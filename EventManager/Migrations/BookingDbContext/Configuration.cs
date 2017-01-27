namespace EventManager.Migrations.BookingDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventManager.Models.BookingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\BookingDbContext";
        }

        protected override void Seed(EventManager.Models.BookingDbContext context)
        {

        }
    }
}
