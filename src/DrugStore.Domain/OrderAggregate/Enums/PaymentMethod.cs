using Ardalis.SmartEnum;

namespace DrugStore.Domain.OrderAggregate.Enums;

public sealed class PaymentMethod : SmartEnum<PaymentMethod>
{
    public static readonly PaymentMethod Cash = new(1, nameof(Cash));
    public static readonly PaymentMethod Online = new(2, nameof(Online));

    private PaymentMethod(int id, string name) : base(name, id)
    {
    }
}