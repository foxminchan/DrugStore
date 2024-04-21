using DrugStore.Application.Users.Queries.GetListQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using DrugStore.Persistence.Helpers;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class List(ISender sender) : IEndpoint<IResult, ListUserRequest>
{
    public async Task<IResult> HandleAsync(
        ListUserRequest request,
        CancellationToken cancellationToken = default)
    {
        FilterHelper filter = new(
            request.Search,
            request.IsAscending,
            null,
            request.PageIndex,
            request.PageSize
        );

        GetListQuery query = new(filter, request.Role);

        var result = await sender.Send(query, cancellationToken);

        var response = new ListUserResponse
        {
            PagedInfo = result.PagedInfo,
            Users = result.Value.Adapt<List<UserDto>>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users", async (
                string? role,
                string? search,
                bool isAscending = true,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, role, search, isAscending)))
            .Produces<ListUserResponse>()
            .WithTags(nameof(User))
            .WithName("List Users")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}