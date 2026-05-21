using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken ct)
    {
        var products = await productRepository.GetAllAsync(ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProductResponse>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var paged = await productRepository.GetPagedAsync(page, pageSize, ct);
        return Ok(ProductMapper.ToPagedResult(paged));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> Search([FromQuery] string term, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest(new { message = "El término de búsqueda es requerido." });

        var products = await productRepository.SearchByNameAsync(term, ct);
        return Ok(products.Select(ProductMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException("Producto", id);

        return Ok(ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] NewProductRequest request, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException("Categoría", request.CategoryId);

        var product = Product.New(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await productRepository.AddAsync(product, ct);
        product = await productRepository.GetByIdAsync(product.Id, ct) ?? product;

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToResponse(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EditProductRequest request, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException("Producto", id);

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, ct);
        if (category is null)
            throw new ResourceNotFoundException("Categoría", request.CategoryId);

        product.Edit(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await productRepository.UpdateAsync(product, ct);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(id, ct);
        if (product is null)
            throw new ResourceNotFoundException("Producto", id);

        await productRepository.DeleteAsync(id, ct);
        return NoContent();
    }
}
