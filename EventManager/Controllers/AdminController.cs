using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //ConfigurationManager.AppSettings["AdminPassword"];
            //Response.SetCookie();
            //Response.Cookies.Add(new HttpCookie("Auth", "4398ghjk28932hjfkd892h3j"));
            //Debug.Write(Response.Cookies["Auth"]);
            return View();
        }

        // POST: Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string adminUsername, string adminPassword)
        {
            if (adminUsername == ConfigurationManager.AppSettings["AdminUsername"] && adminPassword == ConfigurationManager.AppSettings["AdminPassword"])
            {
                Response.Cookies.Add(new HttpCookie("Auth", adminPassword));
                return Redirect("/admin/events");
            }
            return RedirectToAction("Index");
        }
    }
}