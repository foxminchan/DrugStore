using System.Linq.Expressions;
using Ardalis.Specification;

namespace DrugStore.Domain.OrderAggregate.Specifications;

public sealed class OrdersByUserIdSpec : Specification<Order>
{
    public OrdersByUserIdSpec(
        Guid userId,
        int pageNumber,
        int pageSize,
        bool isAscending,
        string? orderBy,
        string? code)
    {
        Query.Where(p => p.CustomerId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(o => o.OrderItems.Select(i => i.Order));

        if (!string.IsNullOrWhiteSpace(code)) Query.Where(o => o.Code!.Contains(code));

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