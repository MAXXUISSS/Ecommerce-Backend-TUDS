using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Orders.Queries;

public record GetOrdersByUserQuery(Guid UserId) : IQuery<IEnumerable<Order>>;
