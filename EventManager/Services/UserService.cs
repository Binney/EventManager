using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EventManager.Services
{
    public static class UserService
    {
        public static bool IsAdmin()
        {
            return HttpContext.Current.Request.Cookies["Auth"]?.Value == ConfigurationManager.AppSettings["AdminPassword"];
        }

        public static bool IsInvitedUser()
        {
            return !(string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["Code"]?.Value));
        }
    }
}