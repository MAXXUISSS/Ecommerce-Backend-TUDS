using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Commands;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(request.Id, ct);
        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, ct);
        if (category is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);

        product.Edit(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await productRepository.UpdateAsync(product, ct);

        return Unit.Value;
    }
}
