using Ardalis.Specification;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Domain.CategoryAggregate.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(CategoryId id) =>
        Query
            .Where(c => c.Id == id)
            .EnableCache(nameof(CategoryId), id);
}