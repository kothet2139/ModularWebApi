using MediatR;
using ModularWebApi.SharedKernel.Domain;

namespace ModularWebApi.Modules.User.Domain.Events
{
    public class UserDisabledEvents : IDomainEvent, INotification
    {
        public DateTime OccurredOn => DateTime.UtcNow;
        public Guid UserId { get; }
        public UserDisabledEvents(Guid userid) 
        {
            UserId = userid;   
        }
    }
}
