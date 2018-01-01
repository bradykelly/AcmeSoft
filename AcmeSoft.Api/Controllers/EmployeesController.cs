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
    public class EmployeesController : BaseController
    {
        public EmployeesController(CompanyContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Only return non-deleted for selection lists, reports etc.
            var emps = Db.Employees.ToList();
            return Ok(emps);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = Db.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (emp == null)
            {
                return Ok(null);
            }
            return Ok(emp);
        }

        [HttpGet("GetByPersonId/{id}")]
        public async Task<IActionResult> GetByPersonId(int id)
        {
            return Ok(await Db.Employees.Where(e => e.PersonId == id).ToListAsync());
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

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            var emp = Db.Employees.SingleOrDefaultAsync(e => e.EmployeeId == id);
            Db.Remove(emp);
            await Db.SaveChangesAsync();
        }
    }
}
