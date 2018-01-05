using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person person)
        {
            Db.Add(person);
            await Db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Person person)
        {
            Db.Update(person);
            await Db.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
