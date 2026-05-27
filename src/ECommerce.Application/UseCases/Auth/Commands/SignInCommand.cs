using MediatR;

namespace ECommerce.Application.UseCases.Auth.Commands;

public record SignInCommand(string Email, string Password) : IRequest<string?>;
