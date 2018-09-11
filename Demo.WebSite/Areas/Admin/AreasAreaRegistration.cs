using System.Web.Mvc;

namespace Demo.WebSite.Areas.Admin
{
    public class AreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{Controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "Demo.WebSite.Areas.Admin.Controllers" }
            );
        }
    }
}
