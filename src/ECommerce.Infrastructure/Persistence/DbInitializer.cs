using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var hashService = scope.ServiceProvider.GetRequiredService<IHashService>();

        await context.Database.MigrateAsync();

        if (!await context.Users.AnyAsync(u => u.Email == "admin@ecommerce.com"))
        {
            var admin = new User(
                "admin@ecommerce.com",
                "Administrador",
                hashService.ComputeHash("Admin@2024"),
                "Admin");
            await context.Users.AddAsync(admin);
        }

        if (!await context.Users.AnyAsync(u => u.Email == "cliente@ecommerce.com"))
        {
            var customer = new User(
                "cliente@ecommerce.com",
                "Cliente Demo",
                hashService.ComputeHash("Cliente@2024"),
                "Customer");
            await context.Users.AddAsync(customer);
        }

        if (!await context.Products.AnyAsync())
        {
            var products = new[]
            {
                Product.New("Laptop HP 15", "Laptop para uso profesional y estudio", 850000m, 8, CategoryConfiguration.ElectronicaId),
                Product.New("Auriculares Sony", "Auriculares inalámbricos con cancelación de ruido", 35000m, 20, CategoryConfiguration.ElectronicaId),
                Product.New("Camisa Oxford", "Camisa de algodón manga larga", 15000m, 40, CategoryConfiguration.RopaId),
                Product.New("Almohada Memory Foam", "Almohada ergonómica viscoelástica", 8500m, 15, CategoryConfiguration.HogarId)
            };
            await context.Products.AddRangeAsync(products);
        }

        await context.SaveChangesAsync();
    }
}
