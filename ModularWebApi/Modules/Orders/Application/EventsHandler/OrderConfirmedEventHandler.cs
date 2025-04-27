using MediatR;
using ModularWebApi.Modules.Orders.Domain.Events;

namespace ModularWebApi.Modules.Orders.Application.EventsHandler
{
    public class OrderConfirmedEventHandler : INotificationHandler<OrderConfirmedEvent>
    {
        public async Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var subject = "Order Confirmation";
            var message = $"Your order {notification.OrderId} has been confirmed.";
        }
    }
}
