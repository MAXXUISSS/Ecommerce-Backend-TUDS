using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.UseCases.Auth;

public class SignUpUseCase(IUserRepository userRepository, IHashService hashService)
{
    public async Task<User> ExecuteAsync(string email, string name, string password, CancellationToken ct = default)
    {
        if (await userRepository.ExistsByEmailAsync(email, ct))
            throw new BusinessException("Ya existe una cuenta registrada con ese email.");

        var hash = hashService.ComputeHash(password);
        var user = new User(email, name, hash, "Customer");
        await userRepository.AddAsync(user, ct);
        return user;
    }
}
