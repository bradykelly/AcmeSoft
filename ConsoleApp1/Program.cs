using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly HttpClient _client = new HttpClient {BaseAddress = new Uri("http://localhost:52304/")};

        static async Task Main(string[] args)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resp = await _client.DeleteAsync("api/Values");
            resp.EnsureSuccessStatusCode();
        }
    }
}
