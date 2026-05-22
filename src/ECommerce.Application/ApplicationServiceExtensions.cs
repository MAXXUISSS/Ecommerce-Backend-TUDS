using ECommerce.Application.UseCases.Auth;
using ECommerce.Application.UseCases.Categories;
using ECommerce.Application.UseCases.Orders;
using ECommerce.Application.UseCases.Products;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SignUpUseCase>();
        services.AddScoped<SignInUseCase>();

        services.AddScoped<PlaceOrderUseCase>();
        services.AddScoped<GetOrdersByUserUseCase>();
        services.AddScoped<GetOrderByIdUseCase>();

        services.AddScoped<GetAllProductsUseCase>();
        services.AddScoped<GetPagedProductsUseCase>();
        services.AddScoped<SearchProductsUseCase>();
        services.AddScoped<GetProductByIdUseCase>();
        services.AddScoped<CreateProductUseCase>();
        services.AddScoped<UpdateProductUseCase>();
        services.AddScoped<DeleteProductUseCase>();

        services.AddScoped<GetAllCategoriesUseCase>();
        services.AddScoped<GetCategoryByIdUseCase>();

        return services;
    }
}
