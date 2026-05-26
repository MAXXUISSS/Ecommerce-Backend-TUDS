using ECommerce.Application.Interfaces;
using ECommerce.Application.UseCases.Products.Queries;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Moq;

namespace ECommerce.Tests.Products;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _repoMock = new();
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _handler = new GetProductByIdQueryHandler(_repoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsProduct_WhenExists()
    {
        var id = Guid.NewGuid();
        var product = Product.New("Laptop", "Descripción", 1500m, 10, Guid.NewGuid());
        _repoMock.Setup(r => r.GetByIdAsync(id, default)).ReturnsAsync(product);

        var result = await _handler.HandleAsync(new GetProductByIdQuery(id));

        Assert.Equal(product, result);
    }

    [Fact]
    public async Task HandleAsync_ThrowsResourceNotFoundException_WhenNotFound()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.GetByIdAsync(id, default)).ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<ResourceNotFoundException>(
            () => _handler.HandleAsync(new GetProductByIdQuery(id)));
    }
}
