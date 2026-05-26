using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.CQRS;
using ECommerce.Application.UseCases.Categories.Queries;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(
    IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>> getAllHandler,
    IQueryHandler<GetCategoryByIdQuery, Category> getByIdHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll(CancellationToken ct)
    {
        var categories = await getAllHandler.HandleAsync(new GetAllCategoriesQuery(), ct);
        return Ok(categories.Select(CategoryMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken ct)
    {
        var category = await getByIdHandler.HandleAsync(new GetCategoryByIdQuery(id), ct);
        return Ok(CategoryMapper.ToResponse(category));
    }
}
