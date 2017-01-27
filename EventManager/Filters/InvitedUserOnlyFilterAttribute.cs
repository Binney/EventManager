using System.Web.Mvc;
using EventManager.Services;

namespace EventManager.Filters
{
    public class InvitedUserOnlyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserService.IsInvitedUser())
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}