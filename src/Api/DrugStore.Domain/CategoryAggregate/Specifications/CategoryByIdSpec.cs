using Ardalis.Specification;

namespace DrugStore.Domain.CategoryAggregate.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(Guid id)
        => Query
            .Where(x => x.Id == id)
            .EnableCache(nameof(CategoryByIdSpec), id);
}
