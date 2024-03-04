using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate;

public sealed class Card : EntityBase
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Card()
    {
    }

    public Card(string? name, string? number, ushort? expiryYear, byte? expiryMonth, ushort? cvc)
    {
        Name = name;
        Number = number;
        ExpiryYear = expiryYear;
        ExpiryMonth = expiryMonth;
        Cvc = cvc;
    }

    public CardId Id { get; set; } = new(Guid.NewGuid());
    public string? Name { get; set; }
    public string? Number { get; set; }
    public ushort? ExpiryYear { get; set; }
    public byte? ExpiryMonth { get; set; }
    public ushort? Cvc { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}