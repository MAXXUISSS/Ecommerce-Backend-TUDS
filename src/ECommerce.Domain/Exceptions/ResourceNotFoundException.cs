namespace ECommerce.Domain.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string resource, Guid id)
        : base($"{resource} con id '{id}' no fue encontrado.") { }

    public ResourceNotFoundException(string message) : base(message) { }
}
