using ECommerce.Application.Interfaces;
using ECommerce.Application.UseCases.Products.Commands;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Moq;

namespace ECommerce.Tests.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private readonly Mock<ICategoryRepository> _categoryRepoMock = new();
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _handler = new CreateProductCommandHandler(_productRepoMock.Object, _categoryRepoMock.Object);
    }

    [Fact]
    public async Task Handle_CreatesProduct_WhenCategoryExists()
    {
        var categoryId = Guid.NewGuid();
        var category = new Category(categoryId, "Electrónica");
        var command = new CreateProductCommand("Laptop", "Descripción", 1500m, 10, categoryId);

        _categoryRepoMock.Setup(r => r.GetByIdAsync(categoryId, default)).ReturnsAsync(category);
        _productRepoMock.Setup(r => r.AddAsync(It.IsAny<Product>(), default)).Returns(Task.CompletedTask);
        _productRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(Product.New(command.Name, command.Description, command.Price, command.Stock, categoryId));

        var result = await _handler.Handle(command, default);

        Assert.Equal("Laptop", result.Name);
        Assert.Equal(1500m, result.Price);
        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>(), default), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenCategoryNotFound()
    {
        var categoryId = Guid.NewGuid();
        var command = new CreateProductCommand("Laptop", "Descripción", 1500m, 10, categoryId);
        _categoryRepoMock.Setup(r => r.GetByIdAsync(categoryId, default)).ReturnsAsync((Category?)null);

        await Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(command, default));

        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>(), default), Times.Never);
    }
}
