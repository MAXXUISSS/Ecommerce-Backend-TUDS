using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products.Queries;

public class SearchProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<SearchProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> HandleAsync(SearchProductsQuery query, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(query.Term))
            throw new BusinessException("El término de búsqueda es requerido.");

        return await productRepository.SearchByNameAsync(query.Term, ct);
    }
}
