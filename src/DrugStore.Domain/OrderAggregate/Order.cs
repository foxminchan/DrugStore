using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.DomainEvents;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate;

public sealed class Order : EntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Order()
    {
    }

    public Order(string? code, IdentityId? customerId)
    {
        Code = code;
        CustomerId = customerId;
    }

    public OrderId Id { get; set; } = new();
    public string? Code { get; set; }
    public IdentityId? CustomerId { get; set; }
    public ApplicationUser? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    public void AddOrder(string key) => RegisterDomainEvent(new OrderCreatedEvent(key));

    public void UpdateOrder(string? code, IdentityId? customerId)
    {
        Code = code;
        CustomerId = customerId;
    }

    private void AddOrderItem(OrderItem item) => OrderItems.Add(item);

    public static class Factory
    {
        public static Order Create(
            IdentityId? id,
            string? code,
            IEnumerable<OrderItem> orderItems)
        {
            Order order = new()
            {
                CustomerId = id,
                Code = code
            };

            foreach (var item in orderItems)
                order.AddOrderItem(item);

            return order;
        }
    }
}