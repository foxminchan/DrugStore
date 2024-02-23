namespace DrugStore.Domain.SharedKernel;

public abstract class EntityBase : HasDomainEventsBase
{
    public Guid Id { get; set; }
}