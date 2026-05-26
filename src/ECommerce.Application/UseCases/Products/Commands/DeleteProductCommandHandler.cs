using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products.Commands;

public class DeleteProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task HandleAsync(DeleteProductCommand command, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), command.Id);

        await productRepository.DeleteAsync(command.Id, ct);
    }
}
