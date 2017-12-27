using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeSoft.Mvc.Data;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
                LastName = pers.LastName,
                FirstName = pers.FirstName,
                BirthDate = pers.BirthDate,
                PersonId = emp.PersonId,
                EmployeeId = emp.EmployeeId,
                EmployeeNum = emp.EmployeeNum,
                EmployedDate = emp.EmployedDate,
                TerminatedDate = emp.TerminatedDate
            });
            return View(emps);
        }

        public IActionResult Create()
        {
            var model = new EmployeeViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel employeeViewModel)
        {
            // NB Validations

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(employeeViewModel);
                var pers = Mapper.Map<Person>(employeeViewModel);
                _dbContext.Add(emp);
                _dbContext.Add(pers);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
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

            return View(model);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(employeeViewModel);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeViewModel.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
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
            return View(model);
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
