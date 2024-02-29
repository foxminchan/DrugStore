using System.Text.Json.Serialization;

using Ardalis.GuardClauses;

using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate;

public sealed class Order : AuditableEntityBase, IAggregateRoot
{
    public string? Code { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Guid? CustomerId { get; set; }
    [JsonIgnore] public ApplicationUser? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    /// <summary>
    /// EF mapping constructor
    /// </summary>
    public Order()
    {
    }

    public Order(string? code, OrderStatus status, PaymentMethod paymentMethod, Guid? customerId)
    {
        Code = code;
        Status = status;
        PaymentMethod = Guard.Against.Null(paymentMethod);
        CustomerId = customerId;
    }
}
