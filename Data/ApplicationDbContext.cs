using Microsoft.EntityFrameworkCore;
using сайт_курсач.Models;

namespace сайт_курсач.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Таблицы базы данных

        public DbSet<Client> Clients { get; set; }

        public DbSet<Master> Masters { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}