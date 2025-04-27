namespace ModularWebApi.Modules.Orders.Application.Commands
{
    public record OrderItemDto(string ProductName, int Quantity, decimal Price);
}
