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

namespace EventManager.Controllers
{
    
    public class HomeController : Controller
    {
        private EventManagerDbContext db = new EventManagerDbContext();

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string userEmail, string invitationCode)
        {
            var invitation = db.Invitations.Where(i => i.Email == userEmail);
            if (invitation.Any() && invitation.First().InvitationCode == invitationCode )
            {
                Response.Cookies.Add(new HttpCookie("Code", invitationCode));
                return RedirectToAction("Upcoming");
            }
            return RedirectToAction("Index");
        }

        [InvitedUserOnlyFilter]
        public ActionResult Upcoming()
        {
            return View(db.Events.Where(e => e.Date > DateTime.Now ).OrderBy(e => e.Date));
        }

     
    }
}