using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Controllers.Base;
using AcmeSoft.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Employee = AcmeSoft.Api.Data.Models.Employee;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcmeSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : BaseController
    {
        public EmployeesController(CompanyContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var emps = Db.Employees.ToList();
            return Ok(JsonConvert.SerializeObject(emps));
        }

        // GET api/<controller>/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            var emp = Db.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            Db.Add(employee);
            await Db.SaveChangesAsync();
            return Ok(employee);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody] Employee employee)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
