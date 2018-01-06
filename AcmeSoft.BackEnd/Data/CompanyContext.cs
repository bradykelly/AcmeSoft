using AcmeSoft.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Employment = AcmeSoft.Shared.Models.Employment;

namespace AcmeSoft.Api.Data
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options): base(options)
        {            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Employment> Employees { get; set; }
        public DbSet<PersonEmployeeDto> PersonEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employment>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId);
        }
    }
}
