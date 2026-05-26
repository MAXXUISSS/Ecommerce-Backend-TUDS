using ECommerce.Application.CQRS;

namespace ECommerce.Application.UseCases.Products.Commands;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId) : ICommand;
