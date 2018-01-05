using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AcmeSoft.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AcmeSoft.Api.Data
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        {            
        }

        public DbSet<PersonEmployeeDto> PersonEmployees { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}