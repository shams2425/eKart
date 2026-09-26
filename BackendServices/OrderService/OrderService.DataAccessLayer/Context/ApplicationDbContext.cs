using Microsoft.EntityFrameworkCore;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.DataAccessLayer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<Order> Orders{ get; set; }
    public DbSet<OrderItem> OrderItem{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItem");

        // Order
        modelBuilder.Entity<Order>()
            .HasKey(ord => ord.OrderID);

        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new
            {
                oi.OrderID,
                oi.ProductID
            });

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);


        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .HasMany(ord => ord.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
