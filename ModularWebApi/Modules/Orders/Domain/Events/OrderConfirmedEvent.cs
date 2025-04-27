using MediatR;
using ModularWebApi.SharedKernel.Domain;

namespace ModularWebApi.Modules.Orders.Domain.Events
{
    public class OrderConfirmedEvent : IDomainEvent , INotification
    {
        public Guid OrderId { get; }
        public Guid UserId { get; }
        public DateTime OccurredOn { get;} = DateTime.UtcNow;
        public OrderConfirmedEvent(Guid orderId, Guid userId) 
        {
            OrderId = orderId;
            UserId = userId;
        }
    }
}
