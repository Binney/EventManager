using System.Web;
using System.Web.Mvc;
using EventManager.Filters;

namespace EventManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
