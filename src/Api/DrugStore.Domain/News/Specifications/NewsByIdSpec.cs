using Ardalis.Specification;

namespace DrugStore.Domain.News.Specifications;

public sealed class NewsByIdSpec : Specification<News>
{
    public NewsByIdSpec(Guid id)
        => Query
            .Where(x => x.Id == id)
            .Include(x => x.Category)
            .EnableCache(nameof(NewsByIdSpec), id);
}
