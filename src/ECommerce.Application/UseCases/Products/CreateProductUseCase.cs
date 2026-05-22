using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Products;

public class CreateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
{
    public async Task<Product> ExecuteAsync(string name, string description, decimal price, int stock, Guid categoryId, CancellationToken ct = default)
    {
        var category = await categoryRepository.GetByIdAsync(categoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), categoryId);

        var product = Product.New(name, description, price, stock, categoryId);
        await productRepository.AddAsync(product, ct);

        return await productRepository.GetByIdAsync(product.Id, ct) ?? product;
    }
}
