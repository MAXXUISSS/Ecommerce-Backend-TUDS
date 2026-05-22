using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Categories;

public class GetCategoryByIdUseCase(ICategoryRepository categoryRepository)
{
    public async Task<Category> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var category = await categoryRepository.GetByIdAsync(id, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), id);

        return category;
    }
}
