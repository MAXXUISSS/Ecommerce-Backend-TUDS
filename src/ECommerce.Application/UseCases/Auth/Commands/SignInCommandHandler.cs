using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Auth.Commands;

public class SignInCommandHandler(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
    : IRequestHandler<SignInCommand, string?>
{
    public async Task<string?> Handle(SignInCommand request, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, ct);
        if (user is null)
            return null;

        if (!hashService.CheckHash(request.Password, user.PasswordHash))
            return null;

        return tokenService.GenerateToken(user);
    }
}
