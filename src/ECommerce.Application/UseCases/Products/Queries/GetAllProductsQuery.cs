using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products.Queries;

public record GetAllProductsQuery() : IQuery<IEnumerable<Product>>;
