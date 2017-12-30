using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcmeSoft.Api.Controllers.Base;
using AcmeSoft.Api.Data;
using AcmeSoft.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AcmeSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : BaseController
    {
        public PersonsController(CompanyContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var emps = Db.Employees.ToList();
            return Ok(JsonConvert.SerializeObject(emps));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pers = await Db.Persons.SingleOrDefaultAsync(p => p.PersonId == id);
            if (pers == null)
            {
                return NotFound();
            }
            return Ok(pers);
        }

        [HttpGet("GetByIdNumber/{idNumber}")]
        public async Task<IActionResult> GetByIdNumber(string idNumber)
        {
            var emp = await Db.Persons.SingleOrDefaultAsync(e => e.IdNumber == idNumber);
            if (emp == null)
            {
                // Avoid unecessary exception processing.
                return Ok(null);
            }
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            Db.Add(person);
            await Db.SaveChangesAsync();
            return Ok(person);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put(Person person)
        {
            Db.Update(person);
            await Db.SaveChangesAsync();
            return Ok(person);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
