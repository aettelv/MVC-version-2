using Labor.Models;
using Microsoft.EntityFrameworkCore;

namespace Labor.DataAccessLayer
{
    public class Sales : DbContext
    {
        public Sales(DbContextOptions<Sales> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().ToTable("Employees");
            base.OnModelCreating(builder);
        }
    }
}