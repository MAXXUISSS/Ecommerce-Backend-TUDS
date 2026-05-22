using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.UseCases.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(
    GetAllCategoriesUseCase getAllCategoriesUseCase,
    GetCategoryByIdUseCase getCategoryByIdUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll(CancellationToken ct)
    {
        var categories = await getAllCategoriesUseCase.ExecuteAsync(ct);
        return Ok(categories.Select(CategoryMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken ct)
    {
        var category = await getCategoryByIdUseCase.ExecuteAsync(id, ct);
        return Ok(CategoryMapper.ToResponse(category));
    }
}
