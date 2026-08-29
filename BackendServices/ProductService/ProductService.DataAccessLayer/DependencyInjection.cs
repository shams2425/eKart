using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccessLayer.Context;
using ProductService.DataAccessLayer.Repositories;
using ProductService.DataAccessLayer.RepositoriesContracts;

namespace ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseMySQL(connectionString);
        });
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
    
}
