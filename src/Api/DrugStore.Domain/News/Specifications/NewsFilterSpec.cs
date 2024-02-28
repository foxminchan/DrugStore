using System.Linq.Expressions;
using Ardalis.Specification;

namespace DrugStore.Domain.News.Specifications;

public sealed class NewsFilterSpec : Specification<News>
{
    public NewsFilterSpec(
        int pageNumber,
        int pageSize,
        bool isAscending,
        string? orderBy,
        string? title)
    {
        Query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(n => n.Category);

        if (!string.IsNullOrWhiteSpace(title))
            Query.Where(n => n.Title!.Contains(title));

        var parameter = Expression.Parameter(typeof(News));
        var lambda = Expression.Lambda<Func<News, object>>(
            Expression.Convert(Expression.Property(parameter, orderBy ?? nameof(News.Id)), typeof(object)),
            parameter);

        if (isAscending)
            Query.OrderBy(lambda!);
        else
            Query.OrderByDescending(lambda!);
    }
}
