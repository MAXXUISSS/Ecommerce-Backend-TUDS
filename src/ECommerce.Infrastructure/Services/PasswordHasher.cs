using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Services;

public class PasswordHasher : IHashService
{
    public string ComputeHash(string plainText)
        => BCrypt.Net.BCrypt.HashPassword(plainText);

    public bool CheckHash(string plainText, string hash)
        => BCrypt.Net.BCrypt.Verify(plainText, hash);
}
