using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products.Commands;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId) : ICommand<Product>;
