using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Employee = AcmeSoft.Api.Data.Models.Employee;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcmeSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        public EmployeesController(CompanyContext dbContext)
        {
            _db = dbContext;
        }

        private readonly CompanyContext _db;

        [HttpGet]
        public IActionResult Get()
        {
            var emps = _db.Employees.ToList();
            return Ok(JsonConvert.SerializeObject(emps));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post(Employee employee)
        {
            _db.Add(employee);
            await _db.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
