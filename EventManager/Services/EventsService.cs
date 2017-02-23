using System;
using System.Collections.Generic;
using System.Linq;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;

namespace EventManager.Services
{
    public static class EventsService
    {
        public static IEnumerable<Event> GetUnbookedEventsInTheFuture(this EventManagerDbContext db)
        {
            return db.Events.Where(m => m.Booking.EventId != m.EventId && m.Date >= DateTime.Now);
        }

        public static void DeleteConfirmedEvent(EventManagerDbContext db, Event deleteEvent)
        {
            var eventInvitations = BookingService.InvitationsToReactivate(db, deleteEvent.Booking);

            if (!eventInvitations.Any())
                return;

            foreach (var invitation in eventInvitations)
            {
                EmailService.CancellationEmail(invitation.Email, invitation.Name, deleteEvent.Name).Wait();
            }
            db.Events.Remove(deleteEvent);
            db.SaveChanges();
        }
    }
}