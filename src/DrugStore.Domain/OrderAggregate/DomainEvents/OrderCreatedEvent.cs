using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate.DomainEvents;

public sealed class OrderCreatedEvent(string key) : DomainEventBase
{
    public string Key { get; set; } = Guard.Against.NullOrEmpty(key);
}