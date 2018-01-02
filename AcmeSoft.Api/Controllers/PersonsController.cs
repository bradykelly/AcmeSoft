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
    public class PersonsController : BaseController
    {
        public PersonsController(CompanyDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Only return non-deleted for selection lists, reports etc.
            var persons = Db.Persons.ToList();
            return Ok(persons);
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
            var pers = await Db.Persons.SingleOrDefaultAsync(e => e.IdNumber == idNumber);
            if (pers == null)
            {
                // Avoid unecessary exception processing.
                return Ok(null);
            }

            return Ok(pers);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            Db.Add(person);
            try
            {
                await Db.SaveChangesAsync();

            }
            catch (DbUpdateException ux)
            {
                var msg = ux.Message;
                throw;
            }
            return Ok(person);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            Db.Update(person);
            await Db.SaveChangesAsync();
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pers = await Db.Persons.SingleOrDefaultAsync(p => p.PersonId == id);
            if (pers == null)
            {
                return NotFound();
            }
            Db.Remove(pers);
            await Db.SaveChangesAsync();
            return Ok();
        }
    }
}
