using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products;

public class GetProductByIdUseCase(IProductRepository productRepository)
{
    public async Task<Product> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), id);

        return product;
    }
}
