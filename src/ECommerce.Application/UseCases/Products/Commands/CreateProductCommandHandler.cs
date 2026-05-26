using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products.Commands;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<CreateProductCommand, Product>
{
    public async Task<Product> HandleAsync(CreateProductCommand command, CancellationToken ct = default)
    {
        var category = await categoryRepository.GetByIdAsync(command.CategoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), command.CategoryId);

        var product = Product.New(command.Name, command.Description, command.Price, command.Stock, command.CategoryId);
        await productRepository.AddAsync(product, ct);

        return await productRepository.GetByIdAsync(product.Id, ct) ?? product;
    }
}
