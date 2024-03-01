namespace DrugStore.Domain.SharedKernel;

public interface IDomainEventContext
{
    IEnumerable<DomainEventBase> GetDomainEvents();
}