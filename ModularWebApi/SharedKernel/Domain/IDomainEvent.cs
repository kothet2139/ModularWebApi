namespace ModularWebApi.SharedKernel.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get;}
    }
}
