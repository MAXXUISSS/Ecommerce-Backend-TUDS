namespace ECommerce.Domain.Exceptions;

public class OutOfStockException : AppException
{
    public OutOfStockException(int requested, int available)
        : base($"Stock insuficiente: se solicitaron {requested} unidades pero solo hay {available} disponibles.") { }
}
