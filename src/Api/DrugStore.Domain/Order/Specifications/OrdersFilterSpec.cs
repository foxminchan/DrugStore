using Ardalis.Specification;
using System.Linq.Expressions;

namespace DrugStore.Domain.Order.Specifications;

public sealed class OrdersFilterSpec : Specification<Order>
{
    public OrdersFilterSpec(
        int pageNumber,
        int pageSize,
        bool isAscending,
        string? orderBy,
        string? code)
    {
        Query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(o => o.OrderItems.Select(i => i.Product));

        if (!string.IsNullOrWhiteSpace(code))
            Query.Where(o => o.Code!.Contains(code));

        var parameter = Expression.Parameter(typeof(Order));
        var lambda = Expression.Lambda<Func<Order, object>>(
            Expression.Convert(Expression.Property(parameter, orderBy ?? nameof(Order.Id)), typeof(object)),
            parameter);

        if (isAscending)
            Query.OrderBy(lambda!);
        else
            Query.OrderByDescending(lambda!);
    }
}
