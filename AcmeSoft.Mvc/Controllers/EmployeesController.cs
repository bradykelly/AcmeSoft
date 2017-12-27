using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeSoft.Mvc.Data;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AcmeSoft.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyContext _dbContext;

        /// <summary>
        /// Instantiates a new <see cref="CompanyContext"/> and injects dependencies where required.
        /// </summary>
        /// <param name="dbContext">A <see cref="DbContext"/> object for injection into the <see cref="CompanyContext"/>.</param>
        public EmployeesController(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var emps = _dbContext.Employees.Join(_dbContext.Persons, emp => emp.PersonId, pers => pers.PersonId, (emp, pers) => new EmployeeViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                LastName = pers.LastName,
                FirstName = pers.FirstName,
                BirthDate = pers.BirthDate,
                PersonId = emp.PersonId,
                EmployeeId = emp.EmployeeId,
                EmployeeNum = emp.EmployeeNum,
                EmployedDate = emp.EmployedDate,
                TerminatedDate = emp.TerminatedDate
            });
            var model = new EmployeeIndexViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = emps
            };
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new EmployeeViewModel
            {
                ModelPurpose = ViewModelPurpose.Create
            };
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel model)
        {
            if (model.TerminatedDate.HasValue && model.TerminatedDate <= model.EmployedDate)
            {
                ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
            }

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(model);
                var pers = Mapper.Map<Person>(model);
                _dbContext.Add(emp);
                _dbContext.Add(pers);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Details", model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _dbContext.Employees.SingleOrDefaultAsync(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<EmployeeViewModel>(emp);

            var pers = await _dbContext.Persons.SingleOrDefaultAsync(e => e.PersonId == model.PersonId);
            if (pers == null)
            {
                return NotFound();
            }
            Mapper.Map(pers, model);

            model.ModelPurpose = ViewModelPurpose.Edit;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel model)
        {
            if (model.TerminatedDate.HasValue && model.TerminatedDate <= model.EmployedDate)
            {
                ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Update(model);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Edit;
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _dbContext.Employees
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<EmployeeViewModel>(emp);
            model.ModelPurpose = ViewModelPurpose.Delete;
            return View("Details", model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _dbContext.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            _dbContext.Employees.Remove(emp);
            var pers = await _dbContext.Persons.SingleOrDefaultAsync(p => p.PersonId == emp.PersonId);
            _dbContext.Persons.Remove(pers);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _dbContext.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
