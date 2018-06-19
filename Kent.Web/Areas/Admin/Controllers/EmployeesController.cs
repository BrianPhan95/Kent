using Kent.Business.Services;
using Kent.Entities.Model;
using Kent.Web.Areas.Admin.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Areas.Admin.Controllers
{
    public class EmployeesController : BackendController
    {
        private readonly IEmployeesServices _employeesServices;

        public EmployeesController(IEmployeesServices employeesServices)
        {
            _employeesServices = employeesServices;
        }

        // GET: Admin/Employees
        public ActionResult Index()
        {
            var employeeList = _employeesServices.GetList();
            List<EmployeesModel> listData = employeeList.Select(d => new EmployeesModel()
            {
                Name = d.Name,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber
            }).ToList();

            return View(listData);
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            var model = new EmployeesModel();
            return View(model);
        }

        // POST: Admin/Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employees employees = new Employees()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Created = DateTime.Now,
                        CreatedBy = "system",
                        RecordActive = true,
                        RecordDeleted = false,
                        RecordOrder = 0
                    };
                    var response = _employeesServices.AddNewEmployees(employees);
                    if (response)
                    {
                        return RedirectToAction("Index");
                    }
                }
                // TODO: Add insert logic here
            }
            catch
            {

            }
            return View(model);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
