using FaultReportingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FaultReportingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<FaultLog> FaultLogs { get; set; }
        public DbSet<HelpDesk> HelpDesks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<SoftwareProduct> SoftwareProducts { get; set; }
        public DbSet<SystemAdministrator> SystemAdministrators { get; set; }
    }
}