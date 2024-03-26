using DrugStore.Application.Users.Commands.UpdateUserInfoCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateInfo(ISender sender) : IEndpoint<UpdateUserInfoResponse, UpdateUserInfoRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/users/info", async (UpdateUserInfoRequest request) => await HandleAsync(request))
            .Produces<UpdateUserInfoResponse>()
            .WithTags(nameof(User))
            .WithName("Update User Information")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateUserInfoResponse> HandleAsync(
        UpdateUserInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(request.Adapt<UpdateUserInfoCommand>(), cancellationToken);
        return new(result.Adapt<UserDto>());
    }
}