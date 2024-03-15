﻿using DrugStore.Application.Users.Queries.GetListQuery;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class List(ISender sender) : IEndpoint<ListUserResponse, ListUserRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users", async (
                int pageIndex,
                int pageSize,
                string? search,
                bool isAscending) => await HandleAsync(new(pageIndex, pageSize, search, isAscending)))
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

        var result = await sender.Send(new GetListQuery(filter), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Users =
            [
                ..result.Value.Select(x => new UserDto(
                    x.Id,
                    x.Email,
                    x.FullName,
                    x.Phone,
                    x.Address
                ))
            ]
        };
    }
}