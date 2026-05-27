using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Commands;

public class PlaceOrderCommandHandler(
    IUserRepository userRepository,
    IProductRepository productRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<PlaceOrderCommand, Order>
{
    public async Task<Order> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User), request.UserId);

        if (request.Lines.Count == 0)
            throw new BusinessException("La orden debe contener al menos un producto.");

        var order = new Order(request.UserId);

        foreach (var line in request.Lines)
        {
            var product = await productRepository.GetByIdAsync(line.ProductId, ct);
            if (product is null)
                throw new NotFoundException(nameof(Product), line.ProductId);

            order.AddItem(product, line.Quantity);
        }

        await orderRepository.AddAsync(order, ct);
        return order;
    }
}
