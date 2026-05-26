using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Orders.Commands;

public record OrderLine(Guid ProductId, int Quantity);

public record PlaceOrderCommand(Guid UserId, List<OrderLine> Lines) : ICommand<Order>;
