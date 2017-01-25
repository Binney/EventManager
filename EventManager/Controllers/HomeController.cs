using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ConfigurationManager.AppSettings["AdminPassword"];
            //Response.SetCookie();
            //Response.Cookies.Add(new HttpCookie("Auth", "4398ghjk28932hjfkd892h3j"));
            //Debug.Write(Response.Cookies["Auth"]);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}