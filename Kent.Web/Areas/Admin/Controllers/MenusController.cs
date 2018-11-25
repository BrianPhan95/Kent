using Kent.Business.Core.Models.Base;
using Kent.Business.Core.Models.Menus;
using Kent.Business.Services.Menus;
using Kent.Libary.Enums;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class MenusController : BackendController
    {
        private readonly IMenuService _menuServices;
        public MenusController(IMenuService menuServices)
        {
            _menuServices = menuServices;
        }

        public ActionResult Index()
        {
            var request = new RequestModel()
            {
                Keyword = string.Empty,
                PageSize = 0,
                PageIndex = 0
            };
            var model = _menuServices.ListingMenu(request);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new MenuManageModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MenuManageModel model, SubmitType submit)
        {
            if (ModelState.IsValid)
            {
                var response = _menuServices.SaveMenu(model);
                if (response.Success)
                {
                    var id = (int)response.Data;
                    switch (submit)
                    {
                        case SubmitType.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id });
                    }
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _menuServices.GetMenuManageModel(id);
            if (!model.Id.HasValue)
            {
                //SetErrorMessage(T("Menu_Message_ObjectNotFound"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MenuManageModel model, SubmitType submit)
        {
            if (ModelState.IsValid)
            {
                var response = _menuServices.SaveMenu(model);
                if (response.Success)
                {
                    var id = (int)response.Data;
                    switch (submit)
                    {
                        case SubmitType.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id });
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(_menuServices.DeleteMenu(id));
        }
    }
}
