using Kent.Libary.Logger;
using Kent.Web.Attribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}