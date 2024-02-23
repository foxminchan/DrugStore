using MediatR;

namespace DrugStore.Domain.SharedKernel;

public sealed class EventWrapper(DomainEventBase @event) : INotification
{
    public DomainEventBase Event { get; } = @event;
}
