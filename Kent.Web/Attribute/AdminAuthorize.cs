using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kent.Web.Attribute
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult(); // Try this but i'm not sure
            filterContext.Result = new RedirectResult("~/Admin");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        /// <summary>
        /// On authorizing
        /// </summary>
        /// <param name="authorizationContext">the authorize context</param>
        //public override void OnAuthorization(AuthorizationContext authorizationContext)
        //{
        //    base.OnAuthorization(authorizationContext);

        //    if (authorizationContext.Result is HttpUnauthorizedResult)
        //    {
        //        throw new KentUnauthorizeException(string.Empty);
        //    }
        //}

        /// <summary>
        /// Authorize
        /// </summary>
        /// <param name="httpContext">the current context</param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var isAuthorize = base.AuthorizeCore(httpContext);
            if (!isAuthorize)
            {
                return false;
            }

            //var userService = HostContainer.GetInstance<IUserService>();

            //if (WorkContext.CurrentUser == null)
            //{
            //    var currentUser = userService.GetActiveUser(httpContext.User.Identity.Name);
            //    if (currentUser == null)
            //    {
            //        FormsAuthentication.SignOut();
            //        return false;
            //    }

            //    // Save this information for getting updates
            //    var lastTimeGettingUpdate = currentUser.LastLogin;

            //    currentUser.LastLogin = DateTime.UtcNow;
            //    userService.UpdateUser(currentUser);

            //    UserSessionModel.SetUserSession(currentUser, lastTimeGettingUpdate);
            //}

            //if (WorkContext.CurrentUser != null)
            //{
                
            //        return true;
            //}

            return false;
        }
    }
}