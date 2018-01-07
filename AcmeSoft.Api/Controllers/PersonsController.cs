using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AcmeSoft.Api.Controllers.Base;
using AcmeSoft.Api.Data;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcmeSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        public PersonsController(CompanyDbContext dbContext)
        {
            Db = dbContext;
        }

        public CompanyDbContext Db;

        [HttpGet]
        [Produces(typeof(IEnumerable<PersonEmployeeDto>))]
        public async Task<IActionResult> Get()
        {
            // NB Make db join only get latest employment vs all.
            return Ok(await Db.PersonEmployees.ToListAsync());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Person))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Db.Persons.SingleOrDefaultAsync(p => p.PersonId == id));
        }

        [HttpGet("GetByIdNumber/{idNumber}")]
        [Produces(typeof(Person))]
        public async Task<IActionResult> GetByIdNumber(string idNumber)
        {
            return Ok(await Db.Persons.SingleOrDefaultAsync(p => p.IdNumber == idNumber));
        }

        [HttpGet("GetEmployees/{id}")]
        [Produces(typeof(IEnumerable<Employment>))]
        public async Task<IActionResult> GetEmployments(int id)
        {
            return Ok(await Db.Employments.Where(e => e.PersonId == id).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person person)
        {
            // NOTE TransactionSCope wouldn't work here for some reason. EF "doesn't support ambient transactions".
            using (var tx = new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
            {
                Db.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
                person.EmployeeNum = BuildEmpNum(person.LastName);
                Db.Add(person);
                await Db.SaveChangesAsync();
                Db.Database.CommitTransaction();
                return Ok(person); 
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Person person)
        {
            Db.Update(person);
            await Db.SaveChangesAsync();
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pers = await Db.Persons.SingleOrDefaultAsync(p => p.PersonId == id);
            Db.Remove(pers);
            await Db.SaveChangesAsync();
            return Ok();
        }

        // This should normally used in a transaction in which the person is created as well.
        private string BuildEmpNum(string lastName)
        {
            var prefix = lastName.Substring(0, Math.Min(3, lastName.Length));
            int serial;
            if (prefix.Length == 3)
            {
                serial = Db.Persons.Count(p => p.LastName.StartsWith(prefix)) + 1;
            }
            else
            {
                serial = Db.Persons.Count(p => p.LastName == prefix) + 1;
            }
            var num = prefix.PadRight(3, '0').ToUpper() + serial.ToString().PadLeft(3, '0');
            return num;
        }
    }
}
