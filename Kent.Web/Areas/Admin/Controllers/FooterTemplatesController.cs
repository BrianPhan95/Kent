using Kent.Business.Core.Models.FooterTemplates;
using Kent.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class FooterTemplatesController : Controller
    {
        private readonly IFooterTemplateServices _footerTemplateServices;

        public FooterTemplatesController(IFooterTemplateServices footerTemplateServices)
        {
            _footerTemplateServices = footerTemplateServices;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var lst = _footerTemplateServices.GetFooterTemplates(string.Empty);
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
            var model = new FooterTemplateManageModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FooterTemplateManageModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var response = _footerTemplateServices.SaveFooterTemplate(model);
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
            FooterTemplateManageModel model = _footerTemplateServices.GetFooterTemplateById(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FooterTemplateManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _footerTemplateServices.SaveFooterTemplate(model);
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
