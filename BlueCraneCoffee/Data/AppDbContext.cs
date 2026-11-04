using BlueCraneCoffee.Models;
using Microsoft.EntityFrameworkCore;

namespace BlueCraneCoffee.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(o => o.Total).HasPrecision(12, 2);
        modelBuilder.Entity<OrderItem>().Property(i => i.UnitPrice).HasPrecision(12, 2);
    }
}
