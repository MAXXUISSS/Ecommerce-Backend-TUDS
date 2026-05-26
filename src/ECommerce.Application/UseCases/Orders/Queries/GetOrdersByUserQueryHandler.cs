using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Orders.Queries;

public class GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
    : IQueryHandler<GetOrdersByUserQuery, IEnumerable<Order>>
{
    public async Task<IEnumerable<Order>> HandleAsync(GetOrdersByUserQuery query, CancellationToken ct = default)
        => await orderRepository.GetByUserIdAsync(query.UserId, ct);
}
