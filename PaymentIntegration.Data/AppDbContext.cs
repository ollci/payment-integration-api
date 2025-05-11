using Microsoft.EntityFrameworkCore;
using PaymentIntegration.Data.Domain;

namespace PaymentIntegration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Balance> Balances => Set<Balance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>()
         .HasKey(b => b.OrderId);

            modelBuilder.Entity<Balance>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Balance)
                .HasForeignKey<Balance>(b => b.OrderId);
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedNever();
            modelBuilder.Entity<Order>()
                .Property(p => p.OrderId)
                .ValueGeneratedNever();
        }
    }
}
