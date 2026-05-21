using ECommerce.Application.UseCases.Auth;
using ECommerce.Application.UseCases.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SignUpUseCase>();
        services.AddScoped<SignInUseCase>();
        services.AddScoped<PlaceOrderUseCase>();
        return services;
    }
}
