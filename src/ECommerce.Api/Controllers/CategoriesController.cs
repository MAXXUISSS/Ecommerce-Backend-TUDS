using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.UseCases.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll(CancellationToken ct)
    {
        var categories = await mediator.Send(new GetAllCategoriesQuery(), ct);
        return Ok(categories.Select(CategoryMapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken ct)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id), ct);
        return Ok(CategoryMapper.ToResponse(category));
    }
}
