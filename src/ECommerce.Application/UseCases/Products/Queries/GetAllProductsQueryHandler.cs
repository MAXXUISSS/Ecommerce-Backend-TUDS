using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Queries;

public class GetAllProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken ct)
        => await productRepository.GetAllAsync(ct);
}
