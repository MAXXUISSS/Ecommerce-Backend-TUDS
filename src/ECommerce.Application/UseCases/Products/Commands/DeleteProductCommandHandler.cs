using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Commands;

public class DeleteProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(request.Id, ct);
        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        await productRepository.DeleteAsync(request.Id, ct);
        return Unit.Value;
    }
}
