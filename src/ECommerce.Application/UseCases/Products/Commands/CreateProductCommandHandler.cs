using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Commands;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId, ct);
        if (category is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);

        var product = Product.New(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await productRepository.AddAsync(product, ct);

        return await productRepository.GetByIdAsync(product.Id, ct) ?? product;
    }
}
