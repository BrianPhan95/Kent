using Kent.Business.Services;
using Kent.Entities.Repositories;
using log4net.Config;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Unity;
using Unity.Mvc5;

namespace Kent.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
            //else
            //{
            //    Response.Redirect("~/Admin");
            //    //FormsAuthentication.LoginUrl = "/Admin";
            //    //FormsAuthentication.RedirectToLoginPage();
            //}
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Config log4net
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));

            // Set database initializer only if site is finish setup 
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebEdEntities, Configuration>());

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterComponents();
            XmlConfigurator.Configure();
        }

        //protected void Application_PreSendRequestHeaders()
        //{
        //    Response.Headers.Remove("Server");
        //    Response.Headers.Remove("X-AspNet-Version");
        //    Response.Headers.Remove("X-AspNetMvc-Version");
        //    Response.Headers.Remove("X-Powered-By");

        //    Response.Headers.Add("Tranfer-Encoding", "Chunked");
        //}

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    Server.ClearError();
        //}

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            #region Repository
            container.RegisterType<IFormRepository, FormRepository>();
            container.RegisterType<IEmployeesRepository, EmployeesRepository>();
            container.RegisterType<IEmailRepository, EmailRepository>();
            container.RegisterType<IEmailQueueRepository, EmailQueueRepository>();
            container.RegisterType<IUserRespository, UserRespository>();

            #endregion

            #region Services
            container.RegisterType<IFormServices, FormServices>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IEmployeesServices, EmployeesServices>();
            container.RegisterType<IEmailServices, EmailServices>();
            container.RegisterType<IEmailQueueServices, EmailQueueServices>();
            #endregion
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
