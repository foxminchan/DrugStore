﻿using Microsoft.AspNetCore.Http;

namespace DrugStore.Persistence.Helpers;

public sealed record FilterHelper(
    string? Search,
    bool IsAscending = true,
    int PageNumber = 1,
    int PageSize = 20)
{
    public static ValueTask<FilterHelper?> BindAsync(HttpContext context)
        => ValueTask.FromResult<FilterHelper?>(new(
            context.Request.Query["Search"],
            !bool.TryParse(context.Request.Query["IsAscending"], out var isAscending) || isAscending,
            int.TryParse(context.Request.Query["PageNumber"], out var pageNumber) ? pageNumber : 1,
            int.TryParse(context.Request.Query["PageSize"], out var pageSize) ? pageSize : 20)
        {
            OrderBy = string.IsNullOrWhiteSpace(context.Request.Query["OrderBy"])
                ? "Id"
                : context.Request.Query["OrderBy"]
        });

    public string? OrderBy { get; init; }

    public FilterHelper WithOrderBy(string? orderBy) => this with { OrderBy = orderBy };
}