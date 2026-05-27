using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Orders.Queries;

public record GetOrderByIdQuery(Guid OrderId, Guid RequestingUserId, bool IsAdmin) : IRequest<Order>;
