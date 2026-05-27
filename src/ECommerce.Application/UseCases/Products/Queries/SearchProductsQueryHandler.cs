using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Queries;

public class SearchProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<SearchProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(SearchProductsQuery request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Term))
            throw new BusinessException("El término de búsqueda es requerido.");

        return await productRepository.SearchByNameAsync(request.Term, ct);
    }
}
