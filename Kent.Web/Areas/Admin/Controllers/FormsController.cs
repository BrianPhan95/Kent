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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Kent.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FormsController : BackendController
    {
        private readonly IFormServices _formServices;
        public FormsController(IFormServices formServices)
        {
            _formServices = formServices;
        }
        // GET: Admin/Forms
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdmissionListing(string keyword, bool? export)
        {
            var listing = _formServices.GetListForms(FormType.Admission, keyword);
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

            if (export.HasValue && export.Value)
            {
                var gv = new GridView();
                gv.DataSource = dataLst;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Danh sach đăng kí nhập học.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View(dataLst);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdvisoryListing(string keyword, bool? export)
        {
            var listing = _formServices.GetListForms(FormType.Advisory, keyword);
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
            if (export.HasValue && export.Value)
            {
                var gv = new GridView();
                gv.DataSource = dataLst;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Danh sách tư vấn.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View(dataLst);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult VisitListing(string keyword, bool? export)
        {
            var listing = _formServices.GetListForms(FormType.Visit, keyword);
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
            if (export.HasValue && export.Value)
            {
                var gv = new GridView();
                gv.DataSource = dataLst;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Danh sách tham quan.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }

            return View(dataLst);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AlumniListing(string keyword, bool? export)
        {
            var listing = _formServices.GetListForms(FormType.Alumni, keyword);
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

            if (export.HasValue && export.Value)
            {
                var gv = new GridView();
                gv.DataSource = dataLst;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Danh sach cuu sinh vien.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }

            return View(dataLst);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ContactListing(string keyword, bool? export)
        {
            var listing = _formServices.GetListForms(FormType.Contact, keyword);
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

            if (export.HasValue && export.Value)
            {
                var gv = new GridView();
                gv.DataSource = dataLst;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Danh sách liên hệ.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }

            return View(dataLst);
        }

        //public void ExportToExcel(List<T> list)
        //{
        //    var gv = new GridView();
        //    gv.DataSource = list;
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";
        //    StringWriter objStringWriter = new StringWriter();
        //    HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        //    gv.RenderControl(objHtmlTextWriter);
        //    Response.Output.Write(objStringWriter.ToString());
        //    Response.Flush();
        //    Response.End();
        //}
    }
}