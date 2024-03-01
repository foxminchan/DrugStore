using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate.DomainEvents;

public class ProductDeletedEvent(Guid id) : DomainEventBase
{
    public Guid ProductId { get; set; } = id;
}