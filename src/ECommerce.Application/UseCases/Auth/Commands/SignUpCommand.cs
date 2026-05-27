using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Auth.Commands;

public record SignUpCommand(string Email, string Name, string Password) : IRequest<User>;
