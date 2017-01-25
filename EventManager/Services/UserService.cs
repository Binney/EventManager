using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Services
{
    public static class UserService
    {
        public static bool IsAdmin()
        {
            return HttpContext.Current.Request.Cookies["Auth"]?.Value == "fdkwjfkldf";
        }
    }
}