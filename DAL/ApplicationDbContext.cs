using Microsoft.EntityFrameworkCore;
using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<CompanyBroker> CompanyBrokers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompanyBroker>()
                .HasKey(bc => new { bc.CompanyId, bc.BrokerId });
            modelBuilder.Entity<CompanyBroker>()
                .HasOne(bc => bc.Company)
                .WithMany(b => b.Brokers)
                .HasForeignKey(bc => bc.CompanyId);
            modelBuilder.Entity<CompanyBroker>()
                .HasOne(bc => bc.Broker)
                .WithMany(c => c.Companies)
                .HasForeignKey(bc => bc.BrokerId);
        }
    }
}
