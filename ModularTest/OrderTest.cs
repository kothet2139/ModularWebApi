using FluentAssertions;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Enum;
using ModularWebApi.Modules.Orders.Domain.Events;

namespace ModularTest
{
    public class OrderTest
    {
        [Fact]
        public void Confirm_ShouldRaise_OrderConfirmedEvent()
        {
            var order = new Order(Guid.NewGuid(), new List<OrderItem>()
            {
                new OrderItem("Laptop", 1, 100)
            });

            order.Confirm();

            order.Status.Should().Be(OrderStatus.Confirmed);

            var domainEvent = order.DomainEvents.FirstOrDefault();
            domainEvent.Should().BeOfType<OrderConfirmedEvent>();
        }
    }
}
