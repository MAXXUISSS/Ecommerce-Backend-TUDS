using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.CQRS;
using ECommerce.Application.UseCases.Auth.Commands;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ICommandHandler<SignUpCommand, User> signUpHandler,
    ICommandHandler<SignInCommand, string?> signInHandler) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register([FromBody] SignUpRequest request, CancellationToken ct)
    {
        var user = await signUpHandler.HandleAsync(new SignUpCommand(request.Email, request.Name, request.Password), ct);
        return Created(string.Empty, UserMapper.ToResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] SignInRequest request, CancellationToken ct)
    {
        var token = await signInHandler.HandleAsync(new SignInCommand(request.Email, request.Password), ct);

        if (token is null)
            return Unauthorized(new { message = "Email o contraseña incorrectos." });

        return Ok(new TokenResponse(token));
    }
}
