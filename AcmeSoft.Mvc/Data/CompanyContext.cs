using AcmeSoft.Models;
using Microsoft.EntityFrameworkCore;
using AcmeSoft.Mvc.ViewModels;

namespace AcmeSoft.Mvc.Data
{
    public class CompanyContext: DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options): base(options)
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
