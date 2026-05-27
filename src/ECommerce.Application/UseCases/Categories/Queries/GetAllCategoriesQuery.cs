using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<Category>>;
