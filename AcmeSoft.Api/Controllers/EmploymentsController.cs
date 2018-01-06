using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Employments")]
    public class EmploymentsController : Controller
    {
        public EmploymentsController(CompanyDbContext dbContext)
        {
            Db = dbContext;
        }

        // NB Make private.
        public CompanyDbContext Db;

        // GET: api/Employments
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employments/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employment employment)
        {
            Db.Add(employment);
            await Db.SaveChangesAsync();
            return Ok();
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
