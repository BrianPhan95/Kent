using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : BackendController
    {
        // GET: Admin/Home
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //if (!IsLogin())
            //{

            //}
            return View();
        }
    }
}