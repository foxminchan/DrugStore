using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate.DomainEvents;

public sealed class ProductDisabledEvent(Guid id) : DomainEventBase
{
    public Guid ProductId { get; set; } = id;
}