using DrugStore.Application.Users.Commands.UpdateUserCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Update(ISender sender) : IEndpoint<UpdateUserResponse, UpdateUserRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/user", async (UpdateUserRequest request) => await HandleAsync(request))
            .Produces<UpdateUserResponse>()
            .WithTags(nameof(User))
            .WithName("Update User")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateUserResponse> HandleAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(request.Adapt<UpdateUserCommand>(), cancellationToken);
        return new(result.Adapt<UserDto>());
    }
}