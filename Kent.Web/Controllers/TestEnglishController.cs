using Kent.Business.Core.Models.Forms;
using Kent.Business.Services;
using Kent.Libary.Enums;
using Kent.Libary.Logger;
using Kent.Libary.Utilities;
using Kent.Web.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Controllers
{
    public class TestEnglishController : BaseController
    {
        private readonly IFormServices _formServices;
        public TestEnglishController(IFormServices formServices)
        {
            _formServices = formServices;
        }

        // GET: Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question1()
        {
            return View();
        }
        public ActionResult Question2()
        {
            return View();
        }

        public ActionResult Question3()
        {
            return View();
        }
        public ActionResult Completed()
        {
            return View();
        }

        public ActionResult Score()
        {
            return View();
        }

        #region Section Audio
        public ActionResult SectionAudio(int section)
        {
            string path = "~/Content/Media/TestEnglish/Audio/Section 1.mp3";
            switch (section)
            {
                case 1:
                    path = "~/Content/Media/TestEnglish/Audio/Section 1.mp3";
                    break;
                case 2:
                    path = "~/Content/Media/TestEnglish/Audio/Section 2.mp3";
                    break;
                case 3:
                    path = "~/Content/Media/TestEnglish/Audio/Section 3.mp3";
                    break;
                case 4:
                    path = "~/Content/Media/TestEnglish/Audio/Section 4.mp3";
                    break;
            }
            var file = Server.MapPath(path);
            return File(file, "audio/mp3");
        }
        
        #endregion
    }
}