using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.DataAccessLayer.Context;
using OrdersService.DataAccessLayer.Repositories;
using OrdersService.DataAccessLayer.RepositoriesContracts;

namespace OrdersService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DbConnection")!;
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        services.AddScoped<IOrderRepository,OrderRepository>();
        return services;
    }

}

