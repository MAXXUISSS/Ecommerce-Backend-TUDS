using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Categories.Queries;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        => await categoryRepository.GetAllAsync(ct);
}
