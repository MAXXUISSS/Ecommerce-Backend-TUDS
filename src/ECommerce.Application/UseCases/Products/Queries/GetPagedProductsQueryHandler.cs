using ECommerce.Application.CQRS;
using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products.Queries;

public class GetPagedProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetPagedProductsQuery, PagedData<Product>>
{
    public async Task<PagedData<Product>> HandleAsync(GetPagedProductsQuery query, CancellationToken ct = default)
        => await productRepository.GetPagedAsync(query.Page, query.PageSize, ct);
}
