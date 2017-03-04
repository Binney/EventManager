using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using EventManager.Services;

namespace EventManager.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (UserService.IsAdmin())
            {
                return Redirect("/admin/events");
            }
            return View(); 
        }

        // POST: Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string adminUsername, string adminPassword)
        {
            if (adminUsername == ConfigurationManager.AppSettings["AdminUsername"] && adminPassword == ConfigurationManager.AppSettings["AdminPassword"])
            {
                Response.Cookies.Add(new HttpCookie("Auth", adminUsername));

                return Redirect("/admin/events");
            }
            ModelState.AddModelError("", "Username or password are incorrect." );
            return View();
        }

        public ActionResult Delete()
        {
            HttpCookie currentAdminCookie = HttpContext.Request.Cookies["Auth"];
            HttpContext.Response.Cookies.Remove("Auth");
            currentAdminCookie.Expires = DateTime.Now.AddDays(-10);
            currentAdminCookie.Value = null;
            HttpContext.Response.SetCookie(currentAdminCookie);
            return Index();
        }
    }
}