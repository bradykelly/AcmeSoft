using AcmeSoft.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        public BaseController(CompanyContext dbContext)
        {
            Db = dbContext;
        }
        
        protected CompanyContext Db;
    }
}