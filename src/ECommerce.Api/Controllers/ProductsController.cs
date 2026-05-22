using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.UseCases.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    GetAllProductsUseCase getAllProductsUseCase,
    GetPagedProductsUseCase getPagedProductsUseCase,
    SearchProductsUseCase searchProductsUseCase,
    GetProductByIdUseCase getProductByIdUseCase,
    CreateProductUseCase createProductUseCase,
    UpdateProductUseCase updateProductUseCase,
    DeleteProductUseCase deleteProductUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken ct)
    {
        var products = await getAllProductsUseCase.ExecuteAsync(ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProductResponse>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var paged = await getPagedProductsUseCase.ExecuteAsync(page, pageSize, ct);
        return Ok(ProductMapper.ToPagedResult(paged));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> Search([FromQuery] string term, CancellationToken ct)
    {
        var products = await searchProductsUseCase.ExecuteAsync(term, ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var product = await getProductByIdUseCase.ExecuteAsync(id, ct);
        return Ok(ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] NewProductRequest request, CancellationToken ct)
    {
        var product = await createProductUseCase.ExecuteAsync(request.Name, request.Description, request.Price, request.Stock, request.CategoryId, ct);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EditProductRequest request, CancellationToken ct)
    {
        await updateProductUseCase.ExecuteAsync(id, request.Name, request.Description, request.Price, request.Stock, request.CategoryId, ct);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await deleteProductUseCase.ExecuteAsync(id, ct);
        return NoContent();
    }
}
