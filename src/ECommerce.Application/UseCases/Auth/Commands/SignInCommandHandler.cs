using ECommerce.Application.CQRS;
using ECommerce.Application.Interfaces;

namespace ECommerce.Application.UseCases.Auth.Commands;

public class SignInCommandHandler(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
    : ICommandHandler<SignInCommand, string?>
{
    public async Task<string?> HandleAsync(SignInCommand command, CancellationToken ct = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, ct);
        if (user is null)
            return null;

        if (!hashService.CheckHash(command.Password, user.PasswordHash))
            return null;

        return tokenService.GenerateToken(user);
    }
}
