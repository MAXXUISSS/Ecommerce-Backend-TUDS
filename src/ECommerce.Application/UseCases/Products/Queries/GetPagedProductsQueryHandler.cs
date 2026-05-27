using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Queries;

public class GetPagedProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetPagedProductsQuery, PagedData<Product>>
{
    public async Task<PagedData<Product>> Handle(GetPagedProductsQuery request, CancellationToken ct)
        => await productRepository.GetPagedAsync(request.Page, request.PageSize, ct);
}
