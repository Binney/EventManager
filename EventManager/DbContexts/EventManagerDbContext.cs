using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EventManager.Areas.Admin.Models;
using EventManager.Models;

namespace EventManager.DbContexts
{
    public class EventManagerDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                        .HasRequired(t => t.Event)
                        .WithOptional(e => e.Booking)
                        .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
    }

    
}

