using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.BusinessLogicLayer.Mappers;
using OrdersService.BusinessLogicLayer.ServiceContracts;
using OrdersService.BusinessLogicLayer.Services;
using OrdersService.BusinessLogicLayer.Validators;

namespace OrdersService.BusinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBussinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();

        services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile).Assembly);
        return services;
    }
}
