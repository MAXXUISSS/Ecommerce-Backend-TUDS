using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Queries;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, Order>
{
    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        var order = await orderRepository.GetByIdWithItemsAsync(request.OrderId, ct);
        if (order is null)
            throw new NotFoundException(nameof(Order), request.OrderId);

        if (!request.IsAdmin && order.UserId != request.RequestingUserId)
            throw new UnauthorizedAccessException("No tienes permiso para ver esta orden.");

        return order;
    }
}
