using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcmeSoft.Gui.Contracts;
using AcmeSoft.Gui.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AcmeSoft.Gui.Services
{
    public class ApiProxy : IApiProxy
    {
        public ApiProxy()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:54954/") };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client;

        #region Persons

        public async Task<Person> CreatePerson(Person person)
        {
            var resp = await _client.PostAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            var json = await _client.GetStringAsync("api/Persons");
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Person>();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
        }

        public async Task<Person> GetPersonByIdNumber(string idNumber)
        {
            var json = await _client.GetStringAsync($"api/Persons/GetByIdNumber/{idNumber}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<Person> GetPersonById(int personId)
        {
            var json = await _client.GetStringAsync($"api/Persons/{personId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            var resp = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Person>(json);
        }

        public async Task DeletePerson(int personId)
        {
            // TODO Check for linked Employee records before delete.
            var resp = await _client.DeleteAsync($"api/Persons/{personId}");
            resp.EnsureSuccessStatusCode();
        }

        #endregion

        public async Task<Employment> CreateEmployment(Employment employment)
        {
            var resp = await _client.PostAsync("api/Employments", new StringContent(JsonConvert.SerializeObject(employment, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Employment>(json);
        }

        public async Task<IEnumerable<Employment>> GetPersonEmployments(int personId)
        {
            var json = await _client.GetStringAsync($"api/Employments/GetByPersonId/{personId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Employment>();
            }
            var emps = JsonConvert.DeserializeObject<IEnumerable<Employment>>(json);
            return emps;
        }

        public async Task<Employment> GetEmploymentByIdAsync(int employmentId)
        {
            var json = await _client.GetStringAsync($"api/Employments/{employmentId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Employment>(json);
        }

        public async Task<Employment> UpdateEmploymentAsync(Employment employment)
        {
            var resp = await _client.PutAsync("api/Employments", new StringContent(JsonConvert.SerializeObject(employment, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Employment>(json);
        }

        public async Task DeleteEmploymentAsync(int employmentId)
        {
            await _client.DeleteAsync($"api/Employments/{employmentId}");
        }
    }
}