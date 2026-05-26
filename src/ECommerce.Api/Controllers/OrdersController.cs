using System.Security.Claims;
using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.CQRS;
using ECommerce.Application.UseCases.Orders.Commands;
using ECommerce.Application.UseCases.Orders.Queries;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController(
    ICommandHandler<PlaceOrderCommand, Order> placeOrderHandler,
    IQueryHandler<GetOrdersByUserQuery, IEnumerable<Order>> getOrdersByUserHandler,
    IQueryHandler<GetOrderByIdQuery, Order> getOrderByIdHandler) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Place([FromBody] PlaceOrderRequest request, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var lines = request.Items
            .Select(i => new OrderLine(i.ProductId, i.Quantity))
            .ToList();

        var order = await placeOrderHandler.HandleAsync(new PlaceOrderCommand(userId, lines), ct);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, OrderMapper.ToResponse(order));
    }

    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetMine(CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var orders = await getOrdersByUserHandler.HandleAsync(new GetOrdersByUserQuery(userId), ct);
        return Ok(orders.Select(OrderMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var isAdmin = User.IsInRole("Admin");
        var order = await getOrderByIdHandler.HandleAsync(new GetOrderByIdQuery(id, userId, isAdmin), ct);
        return Ok(OrderMapper.ToResponse(order));
    }

    private Guid GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(value, out var id))
            throw new UnauthorizedAccessException();
        return id;
    }
}
