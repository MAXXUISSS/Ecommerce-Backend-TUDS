using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Orders.Commands;

public class PlaceOrderCommandHandler(
    IUserRepository userRepository,
    IProductRepository productRepository,
    IOrderRepository orderRepository)
    : ICommandHandler<PlaceOrderCommand, Order>
{
    public async Task<Order> HandleAsync(PlaceOrderCommand command, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, ct);
        if (user is null)
            throw new ResourceNotFoundException(nameof(User), command.UserId);

        if (command.Lines.Count == 0)
            throw new BusinessException("La orden debe contener al menos un producto.");

        var order = new Order(command.UserId);

        foreach (var line in command.Lines)
        {
            var product = await productRepository.GetByIdAsync(line.ProductId, ct);
            if (product is null)
                throw new ResourceNotFoundException(nameof(Product), line.ProductId);

            order.AddItem(product, line.Quantity);
        }

        await orderRepository.AddAsync(order, ct);
        return order;
    }
}
