using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Component.Tools;
using Demo.Component.Tools.Extensions;
using Demo.Site;
using Demo.Site.Imp;
using Demo.Site.Models;
using Demo.Site.Unity;
using Microsoft.Practices.Unity;

namespace Demo.WebSite.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountSiteContract _accountSiteContract = IoCContainer.Current.Resolve<AccountSiteService>();

        //管理后台登录页面
        // GET: /Admin/Login/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/admin");
            }
            return View();
        }

        //登录验证
        //GET:/Admin/Login/DoLogin
        [HttpPost]
        public JsonResult DoLogin(LoginModel model)
        {
            try
            {
                OperationResult result = _accountSiteContract.Login(model);
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                    return Json(new { status = true, msg }, JsonRequestBehavior.AllowGet);
                return Json(new {status = false, msg}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return Json(new { status = false, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
