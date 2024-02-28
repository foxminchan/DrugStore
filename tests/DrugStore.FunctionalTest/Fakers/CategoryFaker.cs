using Bogus;

using DrugStore.Domain.CategoryAggregate;

namespace DrugStore.FunctionalTest.Fakers;

public sealed class CategoryFaker : Faker<Category>
{
    public CategoryFaker()
    {
        RuleFor(c => c.Title, f => f.Commerce.Categories(1)[0]);
        RuleFor(c => c.Link, f => f.Internet.Url());
    }
}
