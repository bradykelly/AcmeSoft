using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcmeSoft.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Employments")]
    public class EmploymentsController : Controller
    {
        public EmploymentsController(CompanyDbContext dbContext)
        {
            _db = dbContext;
        }

        private readonly CompanyDbContext _db;

        // GET: api/Employments
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _db.Employments.SingleOrDefaultAsync(e => e.EmployeeId == id));
        }

        [HttpGet("GetByPersonId/{personId}")]
        public async Task<IActionResult> GetByPersonId(int personId)
        {
            return Ok(await _db.Employments.Where(e => e.PersonId == personId).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employment employment)
        {
            _db.Add(employment);
            await _db.SaveChangesAsync();
            return Ok(employment);
        }
        
        // PUT: api/Employments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
