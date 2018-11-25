using Kent.Business.Core.Models.Users;
using Kent.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Users.UserLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserLogin user = new UserLogin() { Email = model.Email, Password = model.Password };

            var userDetails = _userServices.GetUserDetails(user);

            if (userDetails != null)
            {
                FormsAuthentication.SetAuthCookie(userDetails.Email, false);

                var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddHours(2), false, userDetails.Roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}