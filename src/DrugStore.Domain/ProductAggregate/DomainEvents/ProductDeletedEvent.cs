using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate.DomainEvents;

public sealed class ProductDeletedEvent(ProductId id) : DomainEventBase
{
    public ProductId ProductId { get; set; } = id;
}