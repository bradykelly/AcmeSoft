using Microsoft.EntityFrameworkCore;

namespace DeleteTest
{
    public class DeleteDbContext: DbContext
    {
        public DeleteDbContext(DbContextOptions<DeleteDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}