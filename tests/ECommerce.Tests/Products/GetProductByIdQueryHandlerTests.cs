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
    public async Task Handle_ReturnsProduct_WhenExists()
    {
        var id = Guid.NewGuid();
        var product = Product.New("Laptop", "Descripción", 1500m, 10, Guid.NewGuid());
        _repoMock.Setup(r => r.GetByIdAsync(id, default)).ReturnsAsync(product);

        var result = await _handler.Handle(new GetProductByIdQuery(id), default);

        Assert.Equal(product, result);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenNotFound()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.GetByIdAsync(id, default)).ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(new GetProductByIdQuery(id), default));
    }
}
