using Kent.Libary.Logger;
using Kent.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kent.Web.Controllers
{
    //[InternationalizationAttribute]
    public class BaseController : Controller
    {
        public BaseController()
        {
            //IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();

        }


        //protected override void Initialize(RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //}

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);

        //    //// Save the controller action
        //    //var action = ControllerContext.RouteData.Values["action"].ToString();
        //    //WorkContext.CurrentControllerAction = action;
        //} 

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                try
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                      viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                                 ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(ex);
                }
                return string.Empty;
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = GetDefaultLanguage();
                }
            }
            Language = lang;
            SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }

        #region Language 
        public string Language { get; set; }

        public static List<string> AvailableLanguages = new List<string> { "vn", "en" };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0];
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                System.Web.HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
        #endregion
    }
}