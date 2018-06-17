using Kent.Business.Services;
using Kent.Entities.Repositories;
using log4net.Config;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Mvc5;

namespace Kent.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterComponents();
            XmlConfigurator.Configure();
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Powered-By");

            Response.Headers.Add("Tranfer-Encoding", "Chunked");
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Server.ClearError();
        } 

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            #region Repository
            container.RegisterType<IFormRepository, FormRepository>();
            
            #endregion

            #region Services
            container.RegisterType<IFormServices, FormServices>();
            container.RegisterType<IUserServices, UserServices>();
            #endregion
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
