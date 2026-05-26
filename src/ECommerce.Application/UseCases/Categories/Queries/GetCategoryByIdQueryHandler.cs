using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Categories.Queries;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> HandleAsync(GetCategoryByIdQuery query, CancellationToken ct = default)
    {
        var category = await categoryRepository.GetByIdAsync(query.Id, ct);
        if (category is null)
            throw new ResourceNotFoundException(nameof(Category), query.Id);

        return category;
    }
}
