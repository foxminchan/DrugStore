using DrugStore.Application.Users.Queries.GetByRoleQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class GetByRole(ISender sender) : IEndpoint<GetUserByRoleResponse, GetUserByRoleRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/staff/{isStaff:bool}", async (
                bool isStaff,
                int pageIndex,
                int pageSize,
                string? search,
                bool isAscending) => await HandleAsync(new(isStaff, pageIndex, pageSize, search, isAscending)))
            .WithTags(nameof(User))
            .WithName("Get User By Role")
            .Produces<GetUserByRoleResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<GetUserByRoleResponse> HandleAsync(
        GetUserByRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        FilterHelper filter = new(
            request.Search,
            request.IsAscending,
            null,
            request.PageIndex,
            request.PageSize
        );

        var result = await sender.Send(new GetByRoleQuery(filter, request.IsStaff), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Users = result.Value.Adapt<List<UserDto>>()
        };
    }
}