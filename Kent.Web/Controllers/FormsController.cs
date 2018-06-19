using Kent.Business.Core.Models.Forms;
using Kent.Business.Services;
using Kent.Libary.Enums;
using Kent.Libary.Utilities;
using Kent.Web.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Controllers
{
    public class FormsController : BaseController
    {
        private readonly IFormServices _formServices;
        public FormsController(IFormServices formServices)
        {
            _formServices = formServices;
        }

        // GET: Forms
        public ActionResult Index()
        {
            return View();
        }

        #region Admission
        public ActionResult Admission()
        {
            var model = new AdmissionModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Admission(AdmissionModel model)
        {
            if (ModelState.IsValid)
            {
                var formModel = new FormModel()
                {
                    FormTypeID = FormsEnums.FormType.Admission,
                    Data = SerializeUtilities.Serialize(model),
                    EmailBodyString = RenderRazorViewToString("Admission", model)
                };
                var respone = _formServices.SaveForm(formModel);
                if (respone)
                {
                    return View("Success");
                }
            }
            return View(model);
        }
        #endregion

        #region Advisory
        public ActionResult Advisory()
        {
            var model = new AdvisoryModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Advisory(AdvisoryModel model)
        {
            if (ModelState.IsValid)
            {
                var formModel = new FormModel()
                {
                    FormTypeID = FormsEnums.FormType.Advisory,
                    Data = SerializeUtilities.Serialize(model),
                    EmailBodyString = RenderRazorViewToString("Advisory", model)
                };
                var respone = _formServices.SaveForm(formModel);
                if (respone)
                {
                    return View("Success");
                }
            }
            return View(model);
        }
        #endregion

        #region VisitSchool
        public ActionResult VisitSchool()
        {
            var model = new VisitSchoolModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult VisitSchool(VisitSchoolModel model)
        {
            if (ModelState.IsValid)
            {
                var formModel = new FormModel()
                {
                    FormTypeID = FormsEnums.FormType.Visit,
                    Data = SerializeUtilities.Serialize(model),
                    EmailBodyString = RenderRazorViewToString("VisitSchool", model)
                };
                var respone = _formServices.SaveForm(formModel);
                if (respone)
                {
                    return View("Success");
                }
            }
            return View(model);
        }
        #endregion
    }
}