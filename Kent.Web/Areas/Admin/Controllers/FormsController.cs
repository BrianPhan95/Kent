using Kent.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kent.Libary.Enums;
using Kent.Libary.Utilities;
using Kent.Web.Models.Forms;
using Kent.Web.Areas.Admin.Models.FormData;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class FormsController : BackendController
    {
        private readonly IFormServices _formServices;
        public FormsController(IFormServices formServices)
        {
            _formServices = formServices;
        }
        // GET: Admin/Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdmissionListing()
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Admission);
            List<AdmissionData> dataLst = listing
                .Select(d =>
                {
                    var data = SerializeUtilities.Deserialize<AdmissionModel>(d.Data);
                    return new AdmissionData()
                    {
                        FullName = data.FullName,
                        DateOfBirth = data.DateOfBirth,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        DateSubmit = d.DateSubmit.Value
                    };
                }).ToList();
            return View(dataLst);
        }

        public ActionResult AdvisoryListing()
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Advisory);
            List<AdvisoryData> dataLst = listing
                .Select(d =>
                {
                    var data = SerializeUtilities.Deserialize<AdvisoryModel>(d.Data);
                    return new AdvisoryData()
                    {
                        Name = data.Name,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        DateCreated = d.DateSubmit.Value
                    };
                }).ToList();
            return View(dataLst);
        }
        public ActionResult VisitListing()
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Visit);
            List<VisitSchoolData> dataLst = listing
                .Select(d =>
                {
                    var data = SerializeUtilities.Deserialize<VisitSchoolModel>(d.Data);
                    return new VisitSchoolData()
                    {
                        FullName = data.FullName,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        DateVisit = data.DateVisit,
                        DateCreated = d.DateSubmit.Value
                    };
                }).ToList();

            return View(dataLst);
        }
    }
}