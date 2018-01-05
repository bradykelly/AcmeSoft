using System;
using AcmeSoft.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected virtual string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}