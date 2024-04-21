using DrugStore.Application.Report.Queries.TopProductByMonthQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopProductByMonth(ISender sender) : IEndpoint<IResult, TopProductByMonthRequest>
{
    public async Task<IResult> HandleAsync(TopProductByMonthRequest request,
        CancellationToken cancellationToken = default)
    {
        TopProductsByMonthReport query = new(request.Month, request.Year, request.Limit);
        var result = await sender.Send(query, cancellationToken);
        TopProductByMonthResponse response = new(result.Value);
        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/report/top-product-by-month",
                async (int month, int year, int limit) => await HandleAsync(new(month, year, limit)))
            .Produces<TopProductByMonthResponse>()
            .WithTags(nameof(Report))
            .WithName("Top Product By Month")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}