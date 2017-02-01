using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Filters;
using EventManager.Models;
using EventManager.Services;

namespace EventManager.Areas.Admin.Controllers
{
    [AdminOnlyFilter]
    public class EventsController : Controller
    {
        private EventManagerDbContext db = new EventManagerDbContext();
        // GET: Events
        public ActionResult Index()
        {
            ViewBag.BookedEvents = db.Events.Where(e => e.Booking != null).ToList();
            return View(db.Events.OrderBy(events => events.Date).ToList());
        }

        //Action for sending ajax request to get the upcoming or previous event.
        public ActionResult Filter(string type)
        {
            IEnumerable<Event> events;
            ViewBag.BookedEvents = db.Events.Where(e => e.Booking != null).ToList();

            switch (type)
            {
                case "upcoming":
                    events = db.Events.Where(e => e.Date >= DateTime.Now);
                    break;
                case "previous":
                    events = db.Events.Where(e => e.Date < DateTime.Now);
                    break;
                default:
                    events = db.Events;
                    break;
            }

            return PartialView(events.OrderBy(e => e.Date));
        }

        // GET: Events/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Events/New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "ID,Name,Date,Time")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id); 
            BookingService.EnableInvitation(db, @event.Booking);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
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
