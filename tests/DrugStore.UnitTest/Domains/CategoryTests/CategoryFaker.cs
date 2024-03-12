using Bogus;
using DrugStore.Domain.CategoryAggregate;

namespace DrugStore.UnitTest.Domains.CategoryTests;

public sealed class CategoryFaker : Faker<Category>
{
    public CategoryFaker()
    {
        RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);
        RuleFor(c => c.Description, f => f.Lorem.Sentence());
    }
}