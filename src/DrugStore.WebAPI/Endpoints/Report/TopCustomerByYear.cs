using DrugStore.Application.Report.Queries.TopCustomerByYearQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopCustomerByYear(ISender sender) : IEndpoint<IResult, TopCustomerByYearRequest>
{
    public async Task<IResult> HandleAsync(TopCustomerByYearRequest request,
        CancellationToken cancellationToken = default)
    {
        TopCustomerByYearReport query = new(request.Year, request.Limit);
        var result = await sender.Send(query, cancellationToken);
        TopCustomerByYearResponse response = new(result.Value);
        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/report/top-customer-by-year", async (int year, int limit) => await HandleAsync(new(year, limit)))
            .Produces<TopCustomerByYearResponse>()
            .WithTags(nameof(Report))
            .WithName("Top Customer By Year")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}