using Kent.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kent.Web.Controllers
{
    [InternationalizationAttribute]
    public class BaseController : Controller
    {
        public BaseController()
        {
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            
        }


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //// Save the controller action
            //var action = ControllerContext.RouteData.Values["action"].ToString();
            //WorkContext.CurrentControllerAction = action;
        }
    }
}