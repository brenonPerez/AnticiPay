using AnticiPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnticiPay.Infrastructure.DataAccess;
internal class AnticiPayDbContext : DbContext
{
    public AnticiPayDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Invoices)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Cart)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
