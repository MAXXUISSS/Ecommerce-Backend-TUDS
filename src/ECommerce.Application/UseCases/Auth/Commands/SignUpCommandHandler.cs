using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.UseCases.Auth.Commands;

public class SignUpCommandHandler(IUserRepository userRepository, IHashService hashService)
    : IRequestHandler<SignUpCommand, User>
{
    public async Task<User> Handle(SignUpCommand request, CancellationToken ct)
    {
        if (await userRepository.ExistsByEmailAsync(request.Email, ct))
            throw new BusinessException("Ya existe una cuenta registrada con ese email.");

        var hash = hashService.ComputeHash(request.Password);
        var user = new User(request.Email, request.Name, hash, "Customer");
        await userRepository.AddAsync(user, ct);
        return user;
    }
}
