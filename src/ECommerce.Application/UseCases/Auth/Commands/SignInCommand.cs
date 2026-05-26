using ECommerce.Application.CQRS;

namespace ECommerce.Application.UseCases.Auth.Commands;

public record SignInCommand(string Email, string Password) : ICommand<string?>;
