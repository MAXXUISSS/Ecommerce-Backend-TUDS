using ECommerce.Application.Interfaces;

namespace ECommerce.Application.UseCases.Auth;

public class SignInUseCase(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
{
    public async Task<string?> ExecuteAsync(string email, string password, CancellationToken ct = default)
    {
        var user = await userRepository.GetByEmailAsync(email, ct);
        if (user is null)
            return null;

        if (!hashService.CheckHash(password, user.PasswordHash))
            return null;

        return tokenService.GenerateToken(user);
    }
}
