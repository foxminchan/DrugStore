using DrugStore.Application.Report.Queries.DiffRevenueByMonthQuery;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class DiffRevenueByMonth(ISender sender) : IEndpoint<IResult, DiffRevenueByMonthRequest>
{
    public async Task<IResult> HandleAsync(
        DiffRevenueByMonthRequest request,
        CancellationToken cancellationToken = default)
    {
        DiffRevenueByMonthReport query = new(request.SourceMonth, request.SourceYear, request.TargetMonth,
            request.TargetYear);
        var result = await sender.Send(query, cancellationToken);
        return Results.Ok(result.Value);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/report/diff-revenue-by-month",
                async (int sourceMonth, int sourceYear, int targetMonth, int targetYear) =>
                    await HandleAsync(new(sourceMonth, sourceYear, targetMonth, targetYear)))
            .Produces<DiffRevenueByMonthVm>()
            .WithTags(nameof(Report))
            .WithName("Diff Revenue By Month")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}