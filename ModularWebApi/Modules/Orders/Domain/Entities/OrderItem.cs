namespace ModularWebApi.Modules.Orders.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public string? ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private OrderItem() { }

        public OrderItem(string productName, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Quantity = quantity;
            Price = price;
        }
    }
}
