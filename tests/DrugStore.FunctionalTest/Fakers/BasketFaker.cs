using Bogus;
using DrugStore.Domain.BasketAggregate;

namespace DrugStore.FunctionalTest.Fakers;

public sealed class BasketFaker : Faker<CustomerBasket>
{
    public BasketFaker()
    {
        RuleFor(b => b.Id, f => new(f.Random.Guid()));
        RuleFor(b => b.Items, f =>
        [
            new(new(f.Random.Guid()), f.Commerce.ProductName(), f.Random.Int(1, 50), f.Random.Decimal(1, 1000)),
            new(new(f.Random.Guid()), f.Commerce.ProductName(), f.Random.Int(1, 50), f.Random.Decimal(1, 1000)),
            new(new(f.Random.Guid()), f.Commerce.ProductName(), f.Random.Int(1, 50), f.Random.Decimal(1, 1000)),
            new(new(f.Random.Guid()), f.Commerce.ProductName(), f.Random.Int(1, 50), f.Random.Decimal(1, 1000)),
            new(new(f.Random.Guid()), f.Commerce.ProductName(), f.Random.Int(1, 50), f.Random.Decimal(1, 1000))
        ]);
    }
}