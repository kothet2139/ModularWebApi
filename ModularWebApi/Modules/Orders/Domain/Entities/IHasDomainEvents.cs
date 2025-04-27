using ModularWebApi.SharedKernel.Domain;

namespace ModularWebApi.Modules.Orders.Domain.Entities
{
    public interface IHasDomainEvents
    {
        List<IDomainEvent> DomainEvents { get; }
    }
}
