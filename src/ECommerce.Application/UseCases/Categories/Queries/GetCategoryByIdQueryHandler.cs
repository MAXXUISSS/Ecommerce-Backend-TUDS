using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Categories.Queries;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, ct);
        if (category is null)
            throw new NotFoundException(nameof(Category), request.Id);

        return category;
    }
}
