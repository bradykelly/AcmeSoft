using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using AcmeSoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;

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

        [HttpGet]
        public IActionResult Index()
        {
            var emps = from emp in _dbContext.Employees.Include(e => e.Person)
                       join pers in _dbContext.Persons on emp.PersonId equals pers.PersonId
                       select new EmployeeViewModel
                       {
                           LastName = pers.LastName,
                           FirstName = pers.FirstName,
                           BirthDate = pers.BirthDate.ToString(AppConstants.DefaultDateFormat),
                           PersonId = emp.PersonId,
                           EmployeeId = emp.EmployeeId,
                           EmployeeNum = emp.EmployeeNum,
                           EmployedDate = emp.EmployedDate.ToString(AppConstants.DefaultDateFormat),
                           TerminatedDate = FormatNullableDateTime(emp.TerminatedDate)
                       };

            var model = new EmployeeIndexViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = emps
            };
            return View(model);
        }

        [HttpGet]
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
            if (!string.IsNullOrWhiteSpace(model.TerminatedDate?.Trim()))
            {
                var employed = DateTime.ParseExact(model.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                var terminated = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                if (terminated <= employed)
                {
                    ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                    ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
                }
            }

            if (EmployeeNumExists(model.EmployeeNum))
            {
                ModelState.AddModelError("EmployeeNum", "Employee number already in use");
            }

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(model);
                var pers = Mapper.Map<Person>(model);
                // NB Transaction.
                _dbContext.Add(pers);
                await _dbContext.SaveChangesAsync();
                emp.PersonId = pers.PersonId;
                _dbContext.Add(emp);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Details", model);
        }

        [HttpGet]
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
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.TerminatedDate?.Trim()))
            {
                var employed = DateTime.ParseExact(model.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                var terminated = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                if (terminated <= employed)
                {
                    ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                    ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
                } 
            }

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(model);

                // Couldn't figure how to do this with AutoMapper.
                if (!string.IsNullOrWhiteSpace(model.TerminatedDate))
                {
                    emp.TerminatedDate = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                }

                var pers = Mapper.Map<Person>(model);
                emp.PersonId = pers.PersonId;
                _dbContext.Update(emp);
                _dbContext.Update(pers);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Details", model);
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

        private bool EmployeeNumExists(string employeeNum)
        {
            return _dbContext.Employees.Any(e => e.EmployeeNum == employeeNum);
        }

        private string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}
