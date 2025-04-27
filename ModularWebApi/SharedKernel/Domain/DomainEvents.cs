namespace ModularWebApi.SharedKernel.Domain
{
    public static class DomainEvents
    {
        private static readonly List<IDomainEvent> domainEvents = new();
        public static IReadOnlyList<IDomainEvent> Events => domainEvents.AsReadOnly();

        public static void Raise(IDomainEvent domainEvent) 
        {
            domainEvents.Add(domainEvent);            
        }

        public static void Clear()
        {
            domainEvents.Clear();
        }
    }
}
