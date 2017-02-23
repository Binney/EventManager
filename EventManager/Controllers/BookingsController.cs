using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Filters;
using EventManager.Models;
using EventManager.Services;
using WebGrease.Css.Extensions;

namespace EventManager.Controllers
{
    [InvitedUserOnlyFilter]
    public class BookingsController : Controller
    {
        private EventManagerDbContext db = new EventManagerDbContext();
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult New()
        {
            var events = db.Events.Where(m => m.Booking.EventId != m.EventId && m.Date >= DateTime.Now);
            ViewBag.EventId = new SelectList(events, "EventId", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "EventId,Guest1,Guest2,Guest3")] Booking booking)
        {
            var events = db.Events.Where(m => m.Booking.EventId != m.EventId && m.Date >= DateTime.Now);
            ViewBag.EventId = new SelectList(events, "EventId", "Name");

            //Adds error messages to the booking model if there are any.
            ErrorMessagesService.AddingErrorMessages(ModelState, db, booking);

            if (ModelState.IsValid)
            {
                BookingService.DisableInvitation(db, booking);
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Details", new {id = booking.EventId});
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            
            
            if (BookingService.IsBookedIn(booking))
            {
                return View(booking);
            }
            return RedirectToAction("Index", "Home");
        }

        
        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (BookingService.IsBookedIn(booking))
            {
                BookingService.EnableInvitation(db, booking);
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
            return RedirectToAction("Upcoming", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
