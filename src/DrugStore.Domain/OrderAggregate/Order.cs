using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.DomainEvents;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate;

public sealed class Order : AuditableEntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Order()
    {
    }

    public Order(string? code, OrderStatus? status, PaymentMethod? paymentMethod, IdentityId? customerId)
    {
        Code = code;
        Status = status;
        PaymentMethod = Guard.Against.Null(paymentMethod);
        CustomerId = customerId;
    }

    public OrderId Id { get; set; } = new();
    public string? Code { get; set; }
    public OrderStatus? Status { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public IdentityId? CustomerId { get; set; }
    [JsonIgnore] public ApplicationUser? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    public void AddOrder(string key) => RegisterDomainEvent(new OrderCreatedEvent(key));

    public void UpdateOrder(string? code, OrderStatus status, PaymentMethod paymentMethod, IdentityId? customerId)
    {
        Code = code;
        Status = status;
        PaymentMethod = Guard.Against.Null(paymentMethod);
        CustomerId = customerId;
    }
}