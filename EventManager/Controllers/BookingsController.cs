using System.Net;
using System.Web.Mvc;
using EventManager.DbContexts;
using EventManager.Filters;
using EventManager.Models;
using EventManager.Services;

namespace EventManager.Controllers
{
    [InvitedUserOnlyFilter]
    public class BookingsController : Controller
    {
        private readonly EventManagerDbContext db = new EventManagerDbContext();

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var booking = db.Bookings.Find(id);

            if (booking == null)
                return HttpNotFound();

            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult New()
        {
            var events = db.GetUnbookedEventsInTheFuture();
            ViewBag.EventId = new SelectList(events, "EventId", "Name");
            var booking = new Booking() { PrimaryGuest = Request.Cookies["UserEmail"]?.Value };
            return View(booking);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "EventId,PrimaryGuest,Guest1,Guest2")] Booking booking)
        {
            var events = db.GetUnbookedEventsInTheFuture();
            ViewBag.EventId = new SelectList(events, "EventId", "Name");
            //Adds error messages to the booking model if there are any.
            ErrorMessagesService.CheckForPreviousBookings(ModelState, db, booking);

            if (!ModelState.IsValid)
                return View(booking);

            db.ConfirmBooking(booking);
            
            return RedirectToAction("Details", new {id = booking.EventId});
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var booking = db.Bookings.Find(id);
            
            if (BookingService.IsBookedIn(booking))
                return View(booking);

            return RedirectToAction("Index", "Home");
        }

        
        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var booking = db.Bookings.Find(id);

            if (booking == null)
                return HttpNotFound();

            BookingService.DeleteConfirmedBooking(db, booking);
            
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
