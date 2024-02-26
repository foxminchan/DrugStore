﻿using System.Linq.Expressions;
using Ardalis.Specification;

namespace DrugStore.Domain.Product.Specifications;

public sealed class ProductsFilterSpec : Specification<Product>
{
    public ProductsFilterSpec(
        int pageNumber, 
        int pageSize, 
        bool isAscending,
        string? orderBy,
        string? productName)
    {
        Query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category);

        if (!string.IsNullOrWhiteSpace(productName))
            Query.Where(p => p.Title!.Contains(productName));

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
