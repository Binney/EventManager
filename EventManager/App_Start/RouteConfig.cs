using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
               name: "User",
               url: "upcoming",
               defaults: new { controller = "Home", action = "Upcoming" }
           );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "Manifesto" }
            );

            routes.MapRoute(
                name: "Manifesto",
                url: "manifesto",
                defaults: new { controller = "Home", action = "Manifesto" }
            );

            routes.MapRoute(
                name: "Products",
                url: "products",
                defaults: new { controller = "Home", action = "Products" }
            );

            routes.MapRoute(
                name: "Careers",
                url: "careers",
                defaults: new { controller = "Home", action = "Careers" }
            );

            routes.MapRoute(
                name: "Apply",
                url: "apply",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
