using DrugStore.Domain.Identity;
using DrugStore.Domain.SharedKernel;
using System.Text.Json.Serialization;

namespace DrugStore.Domain.Order;

public sealed class Order(
    string? code,
    bool status,
    PaymentMethod paymentMethod,
    Guid customerId) : AuditableEntityBase, IAggregateRoot
{
    public string? Code { get; set; } = code;
    public OrderStatus Status { get; set; } = status ? OrderStatus.Completed : OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; } = paymentMethod;
    public Guid CustomerId { get; set; } = customerId;
    [JsonIgnore] public ApplicationUser? Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
}
