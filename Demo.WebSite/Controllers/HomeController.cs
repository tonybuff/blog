using System.Collections.Generic;
using System.Web.Mvc;
using Demo.Component.Tools;
using Demo.WebSite.AuthorFiliter;
using Demo.Site.Models;
using Demo.Site;
using Demo.Site.Unity;
using Demo.Site.Imp;
using Microsoft.Practices.Unity;

namespace Demo.WebSite.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IAccountSiteContract _accountSiteContract = IoCContainer.Current.Resolve<AccountSiteService>();

        private readonly IConsumeRecordSiteContract _consumeRecordSiteContract =IoCContainer.Current.Resolve<ConsumeRecordsSiteService>();

        [LoginFilter]
        public ActionResult Index()
        {
            int total;
            var list = _accountSiteContract.GetUserLoginLogPages(UserHelper.Ticket[2], 1, 10, out total).AppendData as List<LoginLogModel>;
            //翻页
            ViewBag.Pager = PagerHelper.CreatePagerByAjax(1, 10, total, "Index.Page",true);
            return View(list);
        }

        public PartialViewResult _Index(int page = 1)
        {
            int total;
            var list = _accountSiteContract.GetUserLoginLogPages(UserHelper.Ticket[2], page, 10, out total).AppendData as List<LoginLogModel>;
            ViewBag.Pager = PagerHelper.CreatePagerByAjax(page, 10, total, "Index.Page",true);
            return PartialView(list);
        }

        public JsonResult GetUserLoginLog(int page=1,int rows=10)
        {
            int total;
            var list = _accountSiteContract.GetUserLoginLogPages(UserHelper.Ticket[2], page, rows, out total).AppendData as List<LoginLogModel>;
            return Json(new {rows=list,total});
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult ConsumeRecord(string consumeType, int page = 1)
        {
            int total;
            var list = _consumeRecordSiteContract.GetConsumeRecordByPage("", page, 5, out total).AppendData as List<ConsumeRecordModel>;
            ViewBag.Pager = PagerHelper.CreatePagerByUrl(page, 5, total,"");
            return View(list);
        }

        public PartialViewResult _ConsumeRecord(string consumeType,int page =1)
        {
            int total;
            var list = _consumeRecordSiteContract.GetConsumeRecordByPage("", page, 5, out total).AppendData as List<ConsumeRecordModel>;
            ViewBag.Pager = PagerHelper.CreatePagerByUrl(page, 5, total, "");
            return PartialView(list);
        }

        public JsonResult SaveConsumeRecord(ConsumeRecordModel model)
        {
            var reult = _consumeRecordSiteContract.Insert(model);
            var error = reult.ResultType.ToString();
            return Json(new {msg = reult.Message}, JsonRequestBehavior.AllowGet);
        }


    }
}
