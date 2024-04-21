using DrugStore.Application.Report.Queries.TotalRevenueByQuarterQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TotalRevenueByQuarter(ISender sender) : IEndpoint<IResult, TotalRevenueByQuarterRequest>
{
    public async Task<IResult> HandleAsync(TotalRevenueByQuarterRequest request,
        CancellationToken cancellationToken = default)
    {
        TotalRevenueByQuarterReport query = new(request.Quarter, request.Year);
        var result = await sender.Send(query, cancellationToken);
        TotalRevenueByQuarterResponse response = new(result.Value);
        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
        => app.MapGet("/report/total-revenue-by-quarter",
                async (int quarter, int year) => await HandleAsync(new(quarter, year)))
            .Produces<TotalRevenueByQuarterResponse>()
            .WithTags(nameof(Report))
            .WithName("Total Revenue By Quarter")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}