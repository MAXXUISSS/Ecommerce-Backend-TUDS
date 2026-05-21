namespace ECommerce.Domain.Exceptions;

public class BusinessException : AppException
{
    public BusinessException(string message) : base(message) { }
}
