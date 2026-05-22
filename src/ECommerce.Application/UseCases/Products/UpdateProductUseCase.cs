using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products;

public class UpdateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
{
    public async Task ExecuteAsync(Guid id, string name, string description, decimal price, int stock, Guid categoryId, CancellationToken ct = default)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException(nameof(Product), id);

        var category = await categoryRepository.GetByIdAsync(categoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), categoryId);

        product.Edit(name, description, price, stock, categoryId);
        await productRepository.UpdateAsync(product, ct);
    }
}
