using System.Web.Mvc;

namespace Demo.WebSite.Areas.Admin
{
    public class AdminFilter:FilterAttribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/admin/login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
             
        }
    }
}