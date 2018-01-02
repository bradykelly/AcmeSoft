using AcmeSoft.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Employee = AcmeSoft.Shared.Models.Employee;

namespace AcmeSoft.Api.Data
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options): base(options)
        {            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId);
        }
    }
}
