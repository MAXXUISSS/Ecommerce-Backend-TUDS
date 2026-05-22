using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products;

public class SearchProductsUseCase(IProductRepository productRepository)
{
    public async Task<IEnumerable<Product>> ExecuteAsync(string term, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(term))
            throw new BusinessException("El término de búsqueda es requerido.");

        return await productRepository.SearchByNameAsync(term, ct);
    }
}
