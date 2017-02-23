using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Filters;
using EventManager.Models;
using EventManager.Services;

namespace EventManager.Controllers
{

    public class HomeController : Controller
    {
        private EventManagerDbContext db = new EventManagerDbContext();

        public ActionResult Index()
        {
            if (Request.Cookies["UserEmail"] == null)
                return View();

            string email = Request.Cookies["UserEmail"]?.Value;

            if (db.Bookings.Any(b => b.Guest1 == email || b.Guest2 == email || b.Guest3 == email))
            {
                Booking booking = db.Bookings.First(b => b.Guest1 == email || b.Guest2 == email || b.Guest3 == email);
                return RedirectToAction("Details", "Bookings", new { id = booking.EventId });
            }

            return RedirectToAction("Upcoming");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string userEmail, string invitationCode)
        {
            if (!InvitationIsValid(userEmail, invitationCode))
            {
                ModelState.AddModelError("", "Email or Invitation Code are incorrect.");
                return View();
            }

            Response.Cookies.Add(new HttpCookie("UserEmail", userEmail));

            if (db.Bookings.AsEnumerable().Any(b => b.AllGuests.Contains(userEmail)))
            {
                Booking booking = db.Bookings.AsEnumerable().First(b => b.AllGuests.Contains(userEmail));
                return RedirectToAction("Details", "Bookings", new { id = booking.EventId });
            }

            return RedirectToAction("Upcoming");
        }

        public bool InvitationIsValid(string userEmail, string invitationCode)
        {
            var invitations = db.Invitations.Where(i => i.Email == userEmail);
            return invitations.Any() && invitations.First().InvitationCode == invitationCode;
        }

        [InvitedUserOnlyFilter]
        public ActionResult Upcoming()
        {
            var userEmail = Request.Cookies["UserEmail"]?.Value;
            ViewBag.NotAlreadyBookedIn = !(db.Bookings.Any(b => b.Guest1 == userEmail || b.Guest2 == userEmail || b.Guest3 == userEmail));
            ViewBag.UnbookedEvents = db.Events.Where(e => e.Booking == null).ToList();
            return View(db.Events.Where(e => e.Date > DateTime.Now).OrderBy(e => e.Date));
        }


    }
}