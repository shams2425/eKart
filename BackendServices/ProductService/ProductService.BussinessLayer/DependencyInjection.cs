using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.BussinessLayer.Mappers;
using ProductService.BussinessLayer.ServiceContracts;
using ProductService.BussinessLayer.Services;
using ProductService.BussinessLayer.Validators;

namespace ProductService.BussinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddbussinessLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductAddRequestToMappingProfile).Assembly);
        services.AddScoped<IProductService, ProductServices>();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        return services;
    }
}
