using DrugStore.Application.Users.Commands.UpdateUserCommand;
using DrugStore.WebAPI.Extensions;
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
        var result = await sender.Send(
            new UpdateUserCommand(
                request.Id,
                request.Email,
                request.Password,
                request.ConfirmPassword,
                request.Role,
                request.FullName,
                request.Phone,
                request.Address
            ), cancellationToken
        );

        var user = result.Value;

        return new(new(
            user.Id,
            user.Email,
            user.FullName,
            user.Phone,
            user.Address
        ));
    }
}