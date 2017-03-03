using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Models;

namespace EventManager.Services
{
    public static class BookingService
    {
        public static void ConfirmBooking(this EventManagerDbContext db, Booking booking)
        {
            var usedInvitations = db.Invitations.Where(i => i.Email == booking.PrimaryGuest || i.Email == booking.Guest1 || i.Email == booking.Guest2);

            if (usedInvitations.Any())
            {
                foreach (Invitation invitation in usedInvitations)
                {
                    invitation.Active = false;
                }
            }

            db.Bookings.Add(booking);
            db.SaveChanges();
        }

        public static void DeleteConfirmedBooking(EventManagerDbContext db, Booking booking)
        {
            if (IsBookedIn(booking))
            {
                EnableInvitation(db, booking);
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
        }

        public static void EnableInvitation(EventManagerDbContext db, Booking booking)
        {
            var reactivatedInvitations = InvitationsToReactivate(db, booking);
            if (booking != null)
            {
                foreach (Invitation invitation in reactivatedInvitations)
                {
                    invitation.Active = true;
                }
            }

        }

        public static bool CheckForPreviousBookings(EventManagerDbContext db, string guest)
        {
            return db.Bookings.Any(b => b.PrimaryGuest == guest || b.Guest1 == guest || b.Guest2 == guest);
        }

        public static IQueryable<Invitation> InvitationsToReactivate(EventManagerDbContext db, Booking booking)
        {
            if (booking != null)
            {
                return
                    db.Invitations.Where(
                        i => i.Email == booking.PrimaryGuest || i.Email == booking.Guest1 || i.Email == booking.Guest2);
            }
            return null;
        } 

        public static bool IsBookedIn(Booking booking)
        {
            string email = HttpContext.Current.Request.Cookies["UserEmail"]?.Value;
            string[] bookingEmails = { booking?.PrimaryGuest, booking?.Guest1, booking?.Guest2 };

            return (bookingEmails.Contains(email) && booking != null);
        }

    }
}