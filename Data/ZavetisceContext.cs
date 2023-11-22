using E_zavetisce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_zavetisce.Data
{
    public class ZavetisceContext : IdentityDbContext<ApplicationUser>
    {
        public ZavetisceContext(DbContextOptions<ZavetisceContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<HandOver> HandOvers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Pet>().ToTable("Pet");
            modelBuilder.Entity<HandOver>().ToTable("HandOver");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Adoption>().ToTable("Adoption");

            modelBuilder.Entity<Adoption>().HasKey(a => new { a.PetID, a.EmployeeID, a.ClientID });
        }
    }
}