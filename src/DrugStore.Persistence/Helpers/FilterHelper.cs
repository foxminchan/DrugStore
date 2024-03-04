using Microsoft.AspNetCore.Http;

namespace DrugStore.Persistence.Helpers;

public sealed record FilterHelper(
    PagingHelper Paging,
    string? Search,
    bool IsAscending = true,
    string? OrderBy = "Id")
{
    public static ValueTask<FilterHelper?> BindAsync(HttpContext context)
        => ValueTask.FromResult<FilterHelper?>(new(
            new(
                int.TryParse(context.Request.Query["PageNumber"], out var pageNumber) ? pageNumber : 1,
                int.TryParse(context.Request.Query["PageSize"], out var pageSize) ? pageSize : 20
            ),
            OrderBy: string.IsNullOrWhiteSpace(context.Request.Query["OrderBy"])
                ? "Id"
                : context.Request.Query["OrderBy"],
            IsAscending: !bool.TryParse(context.Request.Query["IsAscending"], out var isAscending) || isAscending,
            Search: context.Request.Query["Search"]
        ));
}