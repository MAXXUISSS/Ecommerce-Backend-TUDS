using System.Security.Claims;
using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.Interfaces;
using ECommerce.Application.UseCases.Orders;
using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController(PlaceOrderUseCase placeOrderUseCase, IOrderRepository orderRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Place([FromBody] PlaceOrderRequest request, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var lines = request.Items
            .Select(i => new OrderLineInput(i.ProductId, i.Quantity))
            .ToList();

        var order = await placeOrderUseCase.ExecuteAsync(userId, lines, ct);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, OrderMapper.ToResponse(order));
    }

    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetMine(CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var orders = await orderRepository.GetByUserIdAsync(userId, ct);
        return Ok(orders.Select(OrderMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id, CancellationToken ct)
    {
        var order = await orderRepository.GetByIdWithItemsAsync(id, ct);
        if (order is null)
            throw new ResourceNotFoundException("Orden", id);

        var userId = GetCurrentUserId();
        if (!User.IsInRole("Admin") && order.UserId != userId)
            return Forbid();

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
