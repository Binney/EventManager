using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Web;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Models;

namespace EventManager.Services
{
    public static class BookingService
    {
        public static void DisableInvitation(EventManagerDbContext db, Booking booking)
        {
            var usedInvitations = db.Invitations.Where(i => i.Email == booking.Guest1 || i.Email == booking.Guest2 || i.Email == booking.Guest3);

            if (usedInvitations.Any())
            {
                foreach (Invitation invitation in usedInvitations)
                {
                    invitation.Active = false;
                }
            }
        }


        public static bool CheckForPreviousBookings(EventManagerDbContext db, Booking booking)
        {
            string[] emails = { booking.Guest1, booking.Guest2, booking.Guest3 };
            return db.Bookings.Any(b => emails.Contains(b.Guest1) || emails.Contains(b.Guest2) || emails.Contains(b.Guest3));
        }
    }
}