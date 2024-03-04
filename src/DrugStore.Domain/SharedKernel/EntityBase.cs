namespace DrugStore.Domain.SharedKernel;

public abstract class EntityBase : HasDomainEventsBase
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public Guid Version { get; set; } = Guid.NewGuid();
}