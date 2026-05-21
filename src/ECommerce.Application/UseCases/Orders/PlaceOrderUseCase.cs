using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Orders;

public record OrderLineInput(Guid ProductId, int Quantity);

public class PlaceOrderUseCase(
    IUserRepository userRepository,
    IProductRepository productRepository,
    IOrderRepository orderRepository)
{
    public async Task<Order> ExecuteAsync(Guid userId, List<OrderLineInput> lines, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(userId, ct);
        if (user is null)
            throw new ResourceNotFoundException(nameof(User), userId);

        if (lines.Count == 0)
            throw new BusinessException("La orden debe contener al menos un producto.");

        var order = new Order(userId);

        foreach (var line in lines)
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
