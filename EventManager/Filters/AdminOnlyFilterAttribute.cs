using System.Web.Mvc;
using EventManager.Services;

namespace EventManager.Filters
{
    public class AdminOnlyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserService.IsAdmin())
            {
                filterContext.Result = new RedirectResult("/admin");
            }
        }
    }
}