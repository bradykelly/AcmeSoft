using System;
using System.Net.Http;
using System.Net.Http.Headers;
using AcmeSoft.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            Client = new HttpClient { BaseAddress = new Uri("http://localhost:54954/") };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected readonly HttpClient Client;

        protected virtual string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}