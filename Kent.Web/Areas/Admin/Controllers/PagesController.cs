using Kent.Business.Core.Models.Pages;
using Kent.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class PagesController : BackendController
    {
        private readonly IPageServices _pageServices;
        private readonly IHeaderTemplateServices _headerTemplateServices;
        private readonly IFooterTemplateServices _footerTemplateServices;

        public PagesController(IPageServices pageServices, IHeaderTemplateServices headerTemplateServices
            , IFooterTemplateServices footerTemplateServices)
        {
            _pageServices = pageServices;
            _headerTemplateServices = headerTemplateServices;
            _footerTemplateServices = footerTemplateServices;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var lst = _pageServices.GetPages(string.Empty);
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
            var model = new PageManageModel();
            model.HeaderTemplateList = GetHeaderTemplateList();
            model.FooterTemplateList = GetFooterTemplateList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageManageModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var response = _pageServices.SavePage(model);
                    if (response)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }
            catch(Exception ex) 
            {
                LogError(ex);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PageManageModel model = _pageServices.GetPageById(id);
            model.HeaderTemplateList = GetHeaderTemplateList();
            model.FooterTemplateList = GetFooterTemplateList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _pageServices.SavePage(model);
                    if (response)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            model.HeaderTemplateList = GetHeaderTemplateList();
            model.FooterTemplateList = GetFooterTemplateList();
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var response = _pageServices.DeletePage(id);
                return Json(new { success = response }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private IEnumerable<SelectListItem> GetHeaderTemplateList()
        {
            var headerTemplates = _headerTemplateServices.GetHeaderTemplates(string.Empty);
            return headerTemplates.Select(d => new SelectListItem()
            {
                Value = d.Name,
                Text = d.Name,
                Selected = false
            }).ToArray();
        }
        private IEnumerable<SelectListItem> GetFooterTemplateList()
        {
            var footerTemplates = _footerTemplateServices.GetFooterTemplates(string.Empty);
            return footerTemplates.Select(d => new SelectListItem()
            {
                Value = d.Name,
                Text = d.Name,
                Selected = false
            }).ToArray();
        }
    }
}
