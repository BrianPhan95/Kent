using Kent.Business.Core.Models.TestKits;
using Kent.Business.Core.Models.TestKits.Manage;
using Kent.Business.Services.QuestionKits;
using Kent.Business.Services.Questions;
using Kent.Business.Services.QuestionSections;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class TestKitsController : BackendController
    {
        private readonly IQuestionService _questionServices;
        private readonly IQuestionKitService _questionKitService;
        private readonly IQuestionSectionService _questionSectionService;
        public TestKitsController(IQuestionService questionServices,
                               IQuestionKitService questionKitService,
                               IQuestionSectionService questionSectionService)
        {
            _questionServices = questionServices;
            _questionKitService = questionKitService;
            _questionSectionService = questionSectionService;
        }

        public ActionResult Index()
        {
            var model = new List<QuestionKitModel>();
            model = _questionKitService.GetListQuestionKit(new RequestModel());
            return View(model);
        }


        public ActionResult ExampleTestKitImport()
        {
            List<ImportKitFormatModel> dataLst = new List<ImportKitFormatModel>();
            dataLst.Add(new ImportKitFormatModel());

            var gv = new GridView();
            gv.DataSource = dataLst;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Mẫu tạo mới bài test.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            var model = new ImportQuestionKitModel();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Create(ImportQuestionKitModel model)
        //{
        //    DataTable data = GetDataTableFromPostedFile(".xls", model.FilePath);
        //    var result = _questionKitService.ImportTestKit(model, data);
        //    return View();
        //}

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                //if (fileExtension.ToString().ToLower().Equals(".xml"))
                //{
                //    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                //    if (System.IO.File.Exists(fileLocation))
                //    {
                //        System.IO.File.Delete(fileLocation);
                //    }

                //    Request.Files["FileUpload"].SaveAs(fileLocation);
                //    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                //    // DataSet ds = new DataSet();
                //    ds.ReadXml(xmlreader);
                //    xmlreader.Close();
                //}

                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                //    SqlConnection con = new SqlConnection(conn);
                //    string query = "Insert into Person(Name,Email,Mobile) Values('" +
                //    ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() +
                //    "','" + ds.Tables[0].Rows[i][2].ToString() + "')";
                //    con.Open();
                //    SqlCommand cmd = new SqlCommand(query, con);
                //    cmd.ExecuteNonQuery();
                //    con.Close();
                //}
            }
            return View();
        }

        public static DataTable GetDataTableFromPostedFile(string fileExtension, string fileLocation)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string excelConnectionString = string.Empty;
            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            if (fileExtension == ".xls")
            {
                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (fileExtension == ".xlsx")
            {
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {
                excelConnection.Open();
                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                }

                string query = string.Format("Select * from [{0}]", excelSheets[0]);

                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection))
                {
                    dataAdapter.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
    }
}
