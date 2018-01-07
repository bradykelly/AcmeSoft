using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AcmeSoft.Gui.Services
{
    public class ApiProxy
    {
        public ApiProxy()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:54954/") };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client;
    }
}