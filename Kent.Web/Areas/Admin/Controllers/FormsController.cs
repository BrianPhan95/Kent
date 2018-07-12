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
    //[Authorize]
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

        public ActionResult AdmissionListing(string keyword)
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Admission, keyword);
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

        public ActionResult AdvisoryListing(string keyword)
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Advisory, keyword);
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
        public ActionResult VisitListing(string keyword)
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Visit, keyword);
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
        public ActionResult AlumniListing(string keyword)
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Alumni, keyword);
            List<AlumniData> dataLst = listing
                .Select(d =>
                {
                    var data = SerializeUtilities.Deserialize<AlumniModel>(d.Data);
                    return new AlumniData()
                    {
                        FullName = data.FullName,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        GraduatingDate = data.GraduatingDate
                    };
                }).ToList();

            return View(dataLst);
        }

        public ActionResult ContactListing(string keyword)
        {
            var listing = _formServices.GetListForms(FormsEnums.FormType.Contact, keyword);
            List<ContactData> dataLst = listing
                .Select(d =>
                {
                    var data = SerializeUtilities.Deserialize<ContactModel>(d.Data);
                    return new ContactData()
                    {
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        Fullname = data.Fullname,
                        Content = data.Content
                    };
                }).ToList();

            return View(dataLst);
        }
    }
}