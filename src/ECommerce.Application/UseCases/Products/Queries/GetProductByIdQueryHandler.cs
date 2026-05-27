using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Queries;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(request.Id, ct);
        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        return product;
    }
}
