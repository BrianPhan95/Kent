using Kent.Business.Core.Models.HeaderTemplates;
using Kent.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class HeaderTemplatesController : Controller
    {
        private readonly IHeaderTemplateServices _headerTemplateServices;

        public HeaderTemplatesController(IHeaderTemplateServices headerTemplateServices)
        {
            _headerTemplateServices = headerTemplateServices;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var lst = _headerTemplateServices.GetHeaderTemplates(string.Empty);
            return View(lst);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new HeaderTemplateManageModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HeaderTemplateManageModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var response = _headerTemplateServices.SaveHeaderTemplate(model);
                    if (response)
                    {
                        return RedirectToAction("Index");
                    }
                }
               
            }
            catch
            {

            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            HeaderTemplateManageModel model = _headerTemplateServices.GetHeaderTemplateById(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HeaderTemplateManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _headerTemplateServices.SaveHeaderTemplate(model);
                    if (response)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
               
            }
            return View();
        }

       
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
