using ECommerce.Application.CQRS;
using ECommerce.Application.Common;
using ECommerce.Application.UseCases.Auth.Commands;
using ECommerce.Application.UseCases.Categories.Queries;
using ECommerce.Application.UseCases.Orders.Commands;
using ECommerce.Application.UseCases.Orders.Queries;
using ECommerce.Application.UseCases.Products.Commands;
using ECommerce.Application.UseCases.Products.Queries;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Auth commands
        services.AddScoped<ICommandHandler<SignUpCommand, User>, SignUpCommandHandler>();
        services.AddScoped<ICommandHandler<SignInCommand, string?>, SignInCommandHandler>();

        // Product queries
        services.AddScoped<IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsQueryHandler>();
        services.AddScoped<IQueryHandler<GetPagedProductsQuery, PagedData<Product>>, GetPagedProductsQueryHandler>();
        services.AddScoped<IQueryHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();
        services.AddScoped<IQueryHandler<SearchProductsQuery, IEnumerable<Product>>, SearchProductsQueryHandler>();

        // Product commands
        services.AddScoped<ICommandHandler<CreateProductCommand, Product>, CreateProductCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();

        // Category queries
        services.AddScoped<IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>, GetAllCategoriesQueryHandler>();
        services.AddScoped<IQueryHandler<GetCategoryByIdQuery, Category>, GetCategoryByIdQueryHandler>();

        // Order queries
        services.AddScoped<IQueryHandler<GetOrdersByUserQuery, IEnumerable<Order>>, GetOrdersByUserQueryHandler>();
        services.AddScoped<IQueryHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();

        // Order commands
        services.AddScoped<ICommandHandler<PlaceOrderCommand, Order>, PlaceOrderCommandHandler>();

        return services;
    }
}
