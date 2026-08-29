using Microsoft.EntityFrameworkCore;
using ProductService.DataAccessLayer.Entities;

namespace ProductService.DataAccessLayer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
    {
        
    }
    public DbSet<Product> Products { get; set; } 
}
