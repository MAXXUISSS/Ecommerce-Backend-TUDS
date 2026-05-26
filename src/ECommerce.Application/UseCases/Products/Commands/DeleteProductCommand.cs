using ECommerce.Application.CQRS;

namespace ECommerce.Application.UseCases.Products.Commands;

public record DeleteProductCommand(Guid Id) : ICommand;
