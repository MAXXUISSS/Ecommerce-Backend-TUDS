using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Categories;

public class GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
{
    public async Task<IEnumerable<Category>> ExecuteAsync(CancellationToken ct = default)
    {
        return await categoryRepository.GetAllAsync(ct);
    }
}
