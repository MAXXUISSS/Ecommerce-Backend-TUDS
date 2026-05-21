using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Customer";
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public User(string email, string name, string passwordHash, string role = "Customer")
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BusinessException("El email es obligatorio.");
        if (!email.Contains('@'))
            throw new BusinessException("El formato del email no es válido.");
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new BusinessException("La contraseña hasheada es obligatoria.");
        if (role is not "Customer" and not "Admin")
            throw new BusinessException("El rol debe ser Customer o Admin.");

        Id = Guid.NewGuid();
        Email = email.Trim().ToLower();
        Name = name.Trim();
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void AssignRole(string role)
    {
        if (role is not "Customer" and not "Admin")
            throw new BusinessException("El rol debe ser Customer o Admin.");
        Role = role;
    }
}
