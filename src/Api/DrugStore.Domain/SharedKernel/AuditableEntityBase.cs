namespace DrugStore.Domain.SharedKernel;

public class AuditableEntityBase : EntityBase
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid Version { get; set; } = Guid.NewGuid();
}
