using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.CQRS;
using ECommerce.Application.Common;
using ECommerce.Application.UseCases.Products.Commands;
using ECommerce.Application.UseCases.Products.Queries;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> getAllHandler,
    IQueryHandler<GetPagedProductsQuery, PagedData<Product>> getPagedHandler,
    IQueryHandler<SearchProductsQuery, IEnumerable<Product>> searchHandler,
    IQueryHandler<GetProductByIdQuery, Product> getByIdHandler,
    ICommandHandler<CreateProductCommand, Product> createHandler,
    ICommandHandler<UpdateProductCommand> updateHandler,
    ICommandHandler<DeleteProductCommand> deleteHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken ct)
    {
        var products = await getAllHandler.HandleAsync(new GetAllProductsQuery(), ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProductResponse>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var paged = await getPagedHandler.HandleAsync(new GetPagedProductsQuery(page, pageSize), ct);
        return Ok(ProductMapper.ToPagedResult(paged));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> Search([FromQuery] string term, CancellationToken ct)
    {
        var products = await searchHandler.HandleAsync(new SearchProductsQuery(term), ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var product = await getByIdHandler.HandleAsync(new GetProductByIdQuery(id), ct);
        return Ok(ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] NewProductRequest request, CancellationToken ct)
    {
        var product = await createHandler.HandleAsync(
            new CreateProductCommand(request.Name, request.Description, request.Price, request.Stock, request.CategoryId), ct);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EditProductRequest request, CancellationToken ct)
    {
        await updateHandler.HandleAsync(
            new UpdateProductCommand(id, request.Name, request.Description, request.Price, request.Stock, request.CategoryId), ct);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await deleteHandler.HandleAsync(new DeleteProductCommand(id), ct);
        return NoContent();
    }
}
