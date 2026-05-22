using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products;

public class GetPagedProductsUseCase(IProductRepository productRepository)
{
    public async Task<PagedData<Product>> ExecuteAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return await productRepository.GetPagedAsync(page, pageSize, ct);
    }
}
