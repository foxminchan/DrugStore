using Ardalis.SmartEnum;

namespace DrugStore.Domain.OrderAggregate.Enums;

public sealed class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus Pending = new(1, nameof(Pending));
    public static readonly OrderStatus Completed = new(2, nameof(Completed));

    private OrderStatus(int id, string name) : base(name, id)
    {
    }
}