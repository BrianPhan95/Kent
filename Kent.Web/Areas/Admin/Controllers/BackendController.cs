using Kent.Libary.Logger;
using Kent.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BackendController : Controller
    {
        public BackendController()
        {

        }

        public int Permission { get; set; }

        public bool IsLogin()
        {

            return true;
        }

        public void LogError(Exception ex)
        {
            Logger.ErrorException(ex);
        }
    }
}