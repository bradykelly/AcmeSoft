using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Api.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected CompanyDbContext Db;

        protected BaseController(CompanyDbContext dbContext)
        {
            Db = dbContext;
        }
    }
}