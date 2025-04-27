using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.User.Domain.Enum;
using ModularWebApi.Modules.User.Domain.Events;
using ModularWebApi.SharedKernel.Domain;

namespace ModularWebApi.Modules.User.Domain.Entities
{
    public class User : IHasDomainEvents
    {
        public Guid Id { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? Role { get; private set; }
        public UserStatus Status { get; private set; }

        public List<IDomainEvent> DomainEvents { get; } = new();

        private User() { }

        public User(string email, string password, string? role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Status = UserStatus.Active;
            Role = role;
        }

        public void Disabled()
        {
            if (Status != UserStatus.Active)
                throw new InvalidOperationException("User can not be inactive.");

            Status = UserStatus.Inactive;

            var disabledEvent = new UserDisabledEvents(Id);
            DomainEvents.Add(disabledEvent);
        }
    }
}
