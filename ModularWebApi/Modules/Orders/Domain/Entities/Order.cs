using ModularWebApi.Modules.Orders.Domain.Enum;
using ModularWebApi.Modules.Orders.Domain.Events;
using ModularWebApi.SharedKernel.Domain;
using ModularWebApi.Modules.User.Domain.Entities;

namespace ModularWebApi.Modules.Orders.Domain.Entities
{
    public class Order : IHasDomainEvents
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();
        public decimal TotalAmount => Items.Sum(o => o.Price * o.Quantity);
        public OrderStatus Status { get; private set; }
        public List<IDomainEvent> DomainEvents { get; } = new();

        private Order()
        {

        }

        public Order(Guid userId , List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = items;
            Status = OrderStatus.Pending;
        }

        public void Confirm()
        {
            if (Status != OrderStatus.Pending) 
            {
                throw new InvalidOperationException("Order cannot be confirmed.");
            }

            Status = OrderStatus.Confirmed; 

            var confirmedEvent = new OrderConfirmedEvent(Id, UserId);
            DomainEvents.Add(confirmedEvent);
        }
    }
}
