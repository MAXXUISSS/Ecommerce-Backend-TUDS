using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Queries;

public class GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersByUserQuery, IEnumerable<Order>>
{
    public async Task<IEnumerable<Order>> Handle(GetOrdersByUserQuery request, CancellationToken ct)
        => await orderRepository.GetByUserIdAsync(request.UserId, ct);
}
