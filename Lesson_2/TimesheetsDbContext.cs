using Timesheets.Models;
using Microsoft.EntityFrameworkCore;

namespace Timesheets
{
    public class TimesheetsDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public TimesheetsDbContext(DbContextOptions<TimesheetsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
