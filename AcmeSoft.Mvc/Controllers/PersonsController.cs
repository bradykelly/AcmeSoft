using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IApiClient _apiClient;

        /// <summary>
        /// Instantiates a new <see cref="EmployeesController"/> with injected dependencies where required.
        /// </summary>
        /// <param name="apiClient">An <see cref="IApiClient"/> implementation for injection into the <c>controller</c>.</param>
        /// <param name="config">An <see cref="IConfiguration"/> implementation for injection into the <c>controller</c>.</param>
        public PersonsController(IConfiguration config, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _apiClient.BaseAddress = config["Api:Url"];
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _apiClient.GetPersonModelsAsync());
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
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

        // GET: Persons/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Persons/Delete/5
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
    }
}