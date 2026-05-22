using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products;

public class DeleteProductUseCase(IProductRepository productRepository)
{
    public async Task ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), id);

        await productRepository.DeleteAsync(id, ct);
    }
}
