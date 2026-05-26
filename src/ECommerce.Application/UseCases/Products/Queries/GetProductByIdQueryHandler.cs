using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products.Queries;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> HandleAsync(GetProductByIdQuery query, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(query.Id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), query.Id);

        return product;
    }
}
