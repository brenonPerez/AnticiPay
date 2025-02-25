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
}
