using System.Web.Mvc;

namespace Kent.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public string NameSpaces
        {
            get
            {
                return "Kent.Web.Areas.Admin.Controllers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminDefault",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { NameSpaces }
            );
        }
    }
}