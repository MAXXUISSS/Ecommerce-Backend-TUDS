using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Commands;

public record OrderLine(Guid ProductId, int Quantity);

public record PlaceOrderCommand(Guid UserId, List<OrderLine> Lines) : IRequest<Order>;
