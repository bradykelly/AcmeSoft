using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcmeSoft.Gui.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AcmeSoft.Gui.Services
{
    public class ApiProxy
    {
        public ApiProxy()
        {
            Client = new HttpClient { BaseAddress = new Uri("http://localhost:54954/") };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // NB Rename to _client.
        private readonly HttpClient Client;

        #region Persons

        public async Task<Person> CreatePerson(Person person)
        {
            var resp = await Client.PostAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            var json = await Client.GetStringAsync("api/Persons");
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Person>();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
        }

        public async Task<Person> GetPersonByIdNumber(string idNumber)
        {
            var json = await Client.GetStringAsync($"api/Persons/GetByIdNumber/{idNumber}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<Person> GetPersonById(int personId)
        {
            var json = await Client.GetStringAsync($"api/Persons/{personId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<IEnumerable<Employment>> GetPersonEmployments(int personId)
        {
            var json = await Client.GetStringAsync($"api/Persons/GetEmployments/{personId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Employment>();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Employment>>(json);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            var resp = await Client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task DeletePerson(int personId)
        {
            // NB Check for linked Employee records before delete.
            var resp = await Client.DeleteAsync($"api/Persons/{personId}");
            resp.EnsureSuccessStatusCode();
        } 

        #endregion


    }
}