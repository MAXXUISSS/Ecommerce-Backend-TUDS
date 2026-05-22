using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products;

public class GetAllProductsUseCase(IProductRepository productRepository)
{
    public async Task<IEnumerable<Product>> ExecuteAsync(CancellationToken ct = default)
    {
        return await productRepository.GetAllAsync(ct);
    }
}
