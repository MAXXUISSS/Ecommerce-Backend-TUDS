using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Categories.Queries;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> HandleAsync(GetAllCategoriesQuery query, CancellationToken ct = default)
        => await categoryRepository.GetAllAsync(ct);
}
