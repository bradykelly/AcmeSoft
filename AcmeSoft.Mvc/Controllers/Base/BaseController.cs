using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected virtual string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}