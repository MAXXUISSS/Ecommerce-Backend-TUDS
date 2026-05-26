using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Categories.Queries;

public record GetAllCategoriesQuery() : IQuery<IEnumerable<Category>>;
