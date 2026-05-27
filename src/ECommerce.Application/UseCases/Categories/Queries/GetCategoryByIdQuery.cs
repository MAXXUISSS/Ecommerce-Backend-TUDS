using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Category>;
