using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll(CancellationToken ct)
    {
        var categories = await categoryRepository.GetAllAsync(ct);
        return Ok(categories.Select(CategoryMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(id, ct);
        if (category is null)
            throw new ResourceNotFoundException("Categoría", id);

        return Ok(CategoryMapper.ToResponse(category));
    }
}
