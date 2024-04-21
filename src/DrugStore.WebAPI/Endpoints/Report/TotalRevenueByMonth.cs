using DrugStore.Application.Report.Queries.TotalRevenueByMonthQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TotalRevenueByMonth(ISender sender) : IEndpoint<IResult, TotalRevenueByMonthRequest>
{
    public async Task<IResult> HandleAsync(TotalRevenueByMonthRequest request,
        CancellationToken cancellationToken = default)
    {
        TotalRevenueByMonthReport query = new(request.Month, request.Year);
        var result = await sender.Send(query, cancellationToken);
        return Results.Ok(result.Value);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/report/total-revenue-by-month",
                async (int month, int year) => await HandleAsync(new(month, year)))
            .Produces<decimal>()
            .WithTags(nameof(Report))
            .WithName("Total Revenue By Month")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}