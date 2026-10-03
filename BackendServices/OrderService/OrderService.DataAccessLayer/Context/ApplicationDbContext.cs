using Microsoft.EntityFrameworkCore;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.DataAccessLayer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {

    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");

        // Order
        modelBuilder.Entity<Order>()
            .HasKey(ord => ord.OrderID);

        // OrderItem
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

        // Order -> OrderItems
        modelBuilder.Entity<Order>()
            .HasMany(ord => ord.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderID)
            .OnDelete(DeleteBehavior.Cascade);


    }
}