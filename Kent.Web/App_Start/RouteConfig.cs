using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kent.Web
{
    public class RouteConfig
    {
        public static string NameSpaces
        {
            get
            {
                return "Kent.Web.Controllers";
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //   namespaces: new[] { NameSpaces }
            //);

            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { NameSpaces }
           );

            routes.MapMvcAttributeRoutes();
        }
    }
}
