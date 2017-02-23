using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EventManager.Areas.Admin.Models;
using EventManager.DbContexts;
using EventManager.Filters;
using EventManager.Services;

namespace EventManager.Areas.Admin.Controllers
{
    [AdminOnlyFilter]
    public class InvitationsController : Controller
    {
        private EventManagerDbContext db = new EventManagerDbContext();

        // GET: Invitations

        public ActionResult Index()
        {
            return View(db.Invitations.ToList());
        }

        // GET: Invitations/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "ID,Name,Email,InvitationCode")] Invitation invitation)
        {
            if (db.Invitations.Any(i => i.Email == invitation.Email))
            {
                ModelState.AddModelError("Email", "This email has already been sent an invitation");
            }

            if (db.Invitations.Any(i => i.InvitationCode == invitation.InvitationCode))
            {
                ModelState.AddModelError("InvitationCode", "This code already exists");
            }

            if (ModelState.IsValid )
            {
                EmailService.InvitationEmail(invitation.Email, invitation.Name, invitation.InvitationCode).Wait();
                db.Invitations.Add(invitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
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
