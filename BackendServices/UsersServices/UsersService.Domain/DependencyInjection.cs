using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Core.DTOs;
using UsersService.Core.ServiceContracts;
using UsersService.Core.Services;
using UsersService.Core.Validators;

namespace UsersService.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IUserService,UserService>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        return services; 
    }
        
}
