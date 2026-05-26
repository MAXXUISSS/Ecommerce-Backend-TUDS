using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Auth.Commands;

public class SignUpCommandHandler(IUserRepository userRepository, IHashService hashService)
    : ICommandHandler<SignUpCommand, User>
{
    public async Task<User> HandleAsync(SignUpCommand command, CancellationToken ct = default)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email, ct))
            throw new BusinessException("Ya existe una cuenta registrada con ese email.");

        var hash = hashService.ComputeHash(command.Password);
        var user = new User(command.Email, command.Name, hash, "Customer");
        await userRepository.AddAsync(user, ct);
        return user;
    }
}
