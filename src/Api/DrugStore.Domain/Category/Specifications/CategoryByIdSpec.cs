using Ardalis.Specification;

namespace DrugStore.Domain.Category.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(Guid id) 
        => Query
            .Where(x => x.Id == id)
            .EnableCache(nameof(CategoryByIdSpec), id);
}
