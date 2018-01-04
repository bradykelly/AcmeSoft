using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IApiClient _apiClient;

        public EmployeesController(IConfiguration config, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _apiClient.BaseAddress = config["Api:Url"];
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]            
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int personId)
        {
            var emp = new Employee();
            emp.PersonId = personId;
            var model = Mapper.Map<EmployeeViewModel>(emp);
            model.ModelPurpose = ViewModelPurpose.Create;
            return View();
        }

        // POST: Employmee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employmee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employmee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel model)
        {
            // TODO: Add update logic here

            return RedirectToAction(nameof(Index));
        }

        // GET: Employmee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employmee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string BuildEmpNum(string lastName)
        {
            var prefix = lastName.Substring(0, 3);
        }
    }
}