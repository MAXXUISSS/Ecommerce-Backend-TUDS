using ECommerce.Application.CQRS;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Auth.Commands;

public record SignUpCommand(string Email, string Name, string Password) : ICommand<User>;
