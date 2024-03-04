using Microsoft.AspNetCore.Http;

namespace DrugStore.Persistence.Helpers;

public sealed record PagingHelper(
    int PageNumber = 1,
    int PageSize = 20)
{
    public static ValueTask<PagingHelper?> BindAsync(HttpContext context)
        => ValueTask.FromResult<PagingHelper?>(new(
            int.TryParse(context.Request.Query["PageNumber"], out var pageNumber) ? pageNumber : 1,
            int.TryParse(context.Request.Query["PageSize"], out var pageSize) ? pageSize : 20
        ));
}