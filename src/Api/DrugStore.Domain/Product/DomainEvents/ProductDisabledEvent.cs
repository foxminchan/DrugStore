using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Product.DomainEvents;

public sealed class ProductDisabledEvent(Guid id) : DomainEventBase
{
    public Guid ProductId { get; set; } = id;
}
