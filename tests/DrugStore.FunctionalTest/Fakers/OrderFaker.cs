using Bogus;
using DrugStore.Domain.OrderAggregate;

namespace DrugStore.FunctionalTest.Fakers;

public sealed class OrderFaker : Faker<Order>
{
    public OrderFaker()
    {
        RuleFor(x => x.Code, f => f.Random.AlphaNumeric(10));
        RuleFor(x => x.CustomerId, f => new(f.Random.Guid()));
    }
}