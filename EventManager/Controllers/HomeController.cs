using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManager.Areas.Admin.Models;

namespace EventManager.Controllers
{
    public class HomeController : Controller
    {
        private EventDbContext _eventDb = new EventDbContext();
        private InvitationDbContext _invitationDb = new InvitationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string userEmail, string invitationCode)
        {
            var invitation = _invitationDb.Invitations.Where(i => i.Email == userEmail);
            if (invitation.Any() && invitation.First().InvitationCode == invitationCode )
            {
                Response.Cookies.Add(new HttpCookie("Code", invitationCode));
                return RedirectToAction("Upcoming");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Upcoming()
        {
            return View(_eventDb.Events.Where(e => e.Date > DateTime.Now ).OrderBy(e => e.Date));
        }

     
    }
}