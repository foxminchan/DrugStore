using Microsoft.AspNetCore.Http;

namespace DrugStore.Domain.SharedKernel;

public record BaseFilter(
    string? Search,
    int PageNumber = 1,
    int PageSize = 20,
    bool IsAscending = true,
    string? OrderBy = "Id")
{
    public static ValueTask<BaseFilter?> BindAsync(HttpContext context) =>
        ValueTask.FromResult<BaseFilter?>(new(
            PageNumber: int.TryParse(context.Request.Query["PageNumber"], out var pageNumber) ? pageNumber : 1,
            PageSize: int.TryParse(context.Request.Query["PageSize"], out var pageSize) ? pageSize : 20,
            OrderBy: string.IsNullOrWhiteSpace(context.Request.Query["OrderBy"])
                ? "Id"
                : context.Request.Query["OrderBy"],
            IsAscending: !bool.TryParse(context.Request.Query["IsAscending"], out var isAscending) || isAscending,
            Search: context.Request.Query["Search"]
        ));
}