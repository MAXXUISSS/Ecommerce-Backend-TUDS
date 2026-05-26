using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products.Commands;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task HandleAsync(UpdateProductCommand command, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), command.Id);

        var category = await categoryRepository.GetByIdAsync(command.CategoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), command.CategoryId);

        product.Edit(command.Name, command.Description, command.Price, command.Stock, command.CategoryId);
        await productRepository.UpdateAsync(product, ct);
    }
}
