using System.Linq.Expressions;
using Ardalis.Specification;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Domain.ProductAggregate.Specifications;

public sealed class ProductsByCategoryIdSpec : Specification<Product>
{
    public ProductsByCategoryIdSpec(
        CategoryId categoryId,
        int pageNumber,
        int pageSize,
        bool isAscending,
        string? orderBy,
        string? productName)
    {
        Query.Where(p => p.CategoryId == categoryId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category);

        if (!string.IsNullOrWhiteSpace(productName)) Query.Where(p => p.Title!.Contains(productName));

        var parameter = Expression.Parameter(typeof(Product));
        var lambda = Expression.Lambda<Func<Product, object>>(
            Expression.Convert(Expression.Property(parameter, orderBy ?? nameof(Product.Id)), typeof(object)),
            parameter);

        if (isAscending)
            Query.OrderBy(lambda!);
        else
            Query.OrderByDescending(lambda!);
    }
}