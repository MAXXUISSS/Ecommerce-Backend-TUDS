using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Queries;

public record GetOrdersByUserQuery(Guid UserId) : IRequest<IEnumerable<Order>>;
