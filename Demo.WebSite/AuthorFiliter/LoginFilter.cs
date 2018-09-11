using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Site;

namespace Demo.WebSite.AuthorFiliter
{
    /// <summary>
    /// 未登录拦截器
    /// </summary>
    public class LoginFilter:FilterAttribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            if (!UserHelper.IsAuthenticated)
            {
                filterContext.Result=new RedirectResult("account/logon");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
             
        }
    }
}