using Kent.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
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
    }
}