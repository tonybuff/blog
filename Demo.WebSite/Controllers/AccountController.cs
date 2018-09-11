using System;
using System.Web.Mvc;
using System.Web.Security;
using Demo.Component.Tools;
using Demo.Component.Tools.Extensions;
using Demo.Site.Models;
using Demo.Site;
using Demo.Site.Unity;
using Demo.Site.Imp;
using Microsoft.Practices.Unity;

namespace Demo.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountSiteContract _accountSiteContract = IoCContainer.Current.Resolve<AccountSiteService>();

        //登录页面
        // GET: /Account/LogOn
        public ActionResult LogOn(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "" });
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        //登录
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LoginModel model, string returnUrl)
        {
            try
            {
                OperationResult result = _accountSiteContract.Login(model);
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(model.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", msg);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        //退出
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            _accountSiteContract.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
