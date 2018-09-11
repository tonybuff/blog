using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Component.Tools;
using Demo.Core.Models;
using Demo.Site.Imp;
using Microsoft.Practices.Unity;
using Demo.Core;
using Demo.Site.Unity;

namespace Demo.WebSite.Areas.Admin.Controllers
{
    //后台用户管理
    [AdminFilter]
    public class HomeController : Controller
    {

        private readonly IAccountService _accountService = IoCContainer.Current.Resolve<AccountSiteService>();
        
        //用户列表
        // GET: /Areas/Home/
        public ActionResult Index(int? uid,string unicename,int page=1)
        {
            var total = 0;
            int size = 10;
            var list = _accountService.GetUserList(uid, unicename, page, size, out total).AppendData as List<Member>;
            ViewBag.Pager = PagerHelper.CreatePagerByUrl(page, size, total, "");
            return View(list);
        }



    }
}
