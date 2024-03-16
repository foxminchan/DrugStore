using Bogus;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate;

namespace DrugStore.FunctionalTest.Fakers;

public sealed class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.ProductCode, f => f.Random.AlphaNumeric(10));
        RuleFor(p => p.Detail, f => f.Lorem.Sentence());
        RuleFor(p => p.Quantity, f => f.Random.Number(1, 100));
        RuleFor(p => p.CategoryId, f => new CategoryId(f.Random.Guid()));
        RuleFor(p => p.Price, f => new(f.Random.Decimal(500, 1000), f.Random.Decimal(1, 500)));
        RuleFor(p => p.Image, f => new(f.Random.Guid().ToString(), f.Lorem.Sentence(), f.Lorem.Sentence()));
    }
}