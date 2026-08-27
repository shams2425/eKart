using Microsoft.Extensions.DependencyInjection;
using UsersService.Core.RepositoriesContracts;
using UsersService.Infrastructure.DbContext;
using UsersService.Infrastructure.Repositories;

namespace UsersService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection service)
    {
        service.AddSingleton<DapperDbContext>();
        service.AddSingleton<IUserRepository, UserRepository>();

        return service;
    }
}