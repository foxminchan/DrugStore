using DrugStore.Application.Users.Commands.UpdateUserInfoCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateInfo(ISender sender) : IEndpoint<IResult, UpdateUserInfoRequest>
{
    public async Task<IResult> HandleAsync(
        UpdateUserInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.Adapt<UpdateUserInfoCommand>();

        var result = await sender.Send(command, cancellationToken);

        UpdateUserInfoResponse response = new(result.Adapt<UserDto>());

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/users/info", async (UpdateUserInfoRequest request) => await HandleAsync(request))
            .Produces<UpdateUserInfoResponse>()
            .WithTags(nameof(User))
            .WithName("Update User Information")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}