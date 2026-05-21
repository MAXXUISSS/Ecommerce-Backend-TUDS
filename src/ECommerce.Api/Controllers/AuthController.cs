using ECommerce.Api.DTOs;
using ECommerce.Api.Mappers;
using ECommerce.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(SignUpUseCase signUpUseCase, SignInUseCase signInUseCase) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register([FromBody] SignUpRequest request, CancellationToken ct)
    {
        var user = await signUpUseCase.ExecuteAsync(request.Email, request.Name, request.Password, ct);
        return Created(string.Empty, UserMapper.ToResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] SignInRequest request, CancellationToken ct)
    {
        var token = await signInUseCase.ExecuteAsync(request.Email, request.Password, ct);

        if (token is null)
            return Unauthorized(new { message = "Email o contraseña incorrectos." });

        return Ok(new TokenResponse(token));
    }
}
