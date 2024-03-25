using DrugStore.Application.Users.Queries.GetListQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class List(ISender sender) : IEndpoint<ListUserResponse, ListUserRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users", async (
                int pageIndex,
                int pageSize,
                string? role,
                string? search,
                bool isAscending = true) => await HandleAsync(new(pageIndex, pageSize, role, search, isAscending)))
            .WithTags(nameof(User))
            .WithName("List Users")
            .Produces<ListUserResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ListUserResponse> HandleAsync(
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

        var result = await sender.Send(new GetListQuery(filter, request.Role), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Users = result.Value.Adapt<List<UserDto>>()
        };
    }
}