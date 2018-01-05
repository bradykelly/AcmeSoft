using System;
using System.Data.SqlClient;
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

            var conn = new SqlConnection("data source=(local);initial catalog=AcmeSoft;integrated security=SSPI;MultipleActiveResultSets=true;");
        }
    }
}
