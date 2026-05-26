using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Orders.Queries;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    : IQueryHandler<GetOrderByIdQuery, Order>
{
    public async Task<Order> HandleAsync(GetOrderByIdQuery query, CancellationToken ct = default)
    {
        var order = await orderRepository.GetByIdWithItemsAsync(query.OrderId, ct);
        if (order is null)
            throw new ResourceNotFoundException(nameof(Order), query.OrderId);

        if (!query.IsAdmin && order.UserId != query.RequestingUserId)
            throw new UnauthorizedAccessException("No tienes permiso para ver esta orden.");

        return order;
    }
}
