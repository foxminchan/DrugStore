﻿using DrugStore.Application.Users.Commands.UpdateUserCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateUserRequest>
{
    public async Task<IResult> HandleAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.Adapt<UpdateUserCommand>();

        var result = await sender.Send(command, cancellationToken);

        UpdateUserResponse response = new(result.Adapt<UserDto>());

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/users", async (UpdateUserRequest request) => await HandleAsync(request))
            .Produces<UpdateUserResponse>()
            .WithTags(nameof(User))
            .WithName("Update User")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}