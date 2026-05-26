using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Orders.Queries;

public record GetOrderByIdQuery(Guid OrderId, Guid RequestingUserId, bool IsAdmin) : IQuery<Order>;
