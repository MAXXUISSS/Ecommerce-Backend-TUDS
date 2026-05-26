using ECommerce.Application.Interfaces;
using ECommerce.Application.UseCases.Orders.Commands;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Moq;

namespace ECommerce.Tests.Orders;

public class PlaceOrderCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private readonly Mock<IOrderRepository> _orderRepoMock = new();
    private readonly PlaceOrderCommandHandler _handler;

    public PlaceOrderCommandHandlerTests()
    {
        _handler = new PlaceOrderCommandHandler(_userRepoMock.Object, _productRepoMock.Object, _orderRepoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_CreatesOrder_WhenValid()
    {
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var user = new User("cliente@test.com", "Cliente", "hash", "Customer");
        var product = Product.New("Laptop", "Descripción", 1500m, 10, categoryId);

        _userRepoMock.Setup(r => r.GetByIdAsync(userId, default)).ReturnsAsync(user);
        _productRepoMock.Setup(r => r.GetByIdAsync(productId, default)).ReturnsAsync(product);
        _orderRepoMock.Setup(r => r.AddAsync(It.IsAny<Order>(), default)).Returns(Task.CompletedTask);

        var command = new PlaceOrderCommand(userId, [new OrderLine(productId, 2)]);

        var result = await _handler.HandleAsync(command);

        Assert.Equal(userId, result.UserId);
        Assert.Single(result.Items);
        _orderRepoMock.Verify(r => r.AddAsync(It.IsAny<Order>(), default), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ThrowsBusinessException_WhenNoLines()
    {
        var userId = Guid.NewGuid();
        var user = new User("cliente@test.com", "Cliente", "hash", "Customer");
        _userRepoMock.Setup(r => r.GetByIdAsync(userId, default)).ReturnsAsync(user);

        var command = new PlaceOrderCommand(userId, []);

        await Assert.ThrowsAsync<BusinessException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task HandleAsync_ThrowsResourceNotFoundException_WhenUserNotFound()
    {
        var userId = Guid.NewGuid();
        _userRepoMock.Setup(r => r.GetByIdAsync(userId, default)).ReturnsAsync((User?)null);

        var command = new PlaceOrderCommand(userId, [new OrderLine(Guid.NewGuid(), 1)]);

        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.HandleAsync(command));
    }
}
