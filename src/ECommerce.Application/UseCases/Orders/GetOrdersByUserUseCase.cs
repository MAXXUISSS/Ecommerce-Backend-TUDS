using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Orders;

public class GetOrdersByUserUseCase(IOrderRepository orderRepository)
{
    public async Task<IEnumerable<Order>> ExecuteAsync(Guid userId, CancellationToken ct = default)
    {
        return await orderRepository.GetByUserIdAsync(userId, ct);
    }
}
