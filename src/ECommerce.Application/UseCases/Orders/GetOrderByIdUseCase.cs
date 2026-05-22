using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Orders;

public class GetOrderByIdUseCase(IOrderRepository orderRepository)
{
    public async Task<Order> ExecuteAsync(Guid orderId, Guid requestingUserId, bool isAdmin, CancellationToken ct = default)
    {
        var order = await orderRepository.GetByIdWithItemsAsync(orderId, ct);
        if (order is null)
            throw new ResourceNotFoundException(nameof(Order), orderId);

        if (!isAdmin && order.UserId != requestingUserId)
            throw new UnauthorizedAccessException("No tienes permiso para ver esta orden.");

        return order;
    }
}
