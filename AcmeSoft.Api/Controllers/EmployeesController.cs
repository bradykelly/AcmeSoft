using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Controllers.Base;
using AcmeSoft.Api.Data;
using AcmeSoft.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("{id}")]
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            Db.Update(employee);
            await Db.SaveChangesAsync();
            return Ok(employee);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
