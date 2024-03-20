using DrugStore.Application.Users.Commands.DeleteUserCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteUserRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/users/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .WithTags(nameof(User))
            .WithName("Delete User")
            .Produces<Unit>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<Unit> HandleAsync(DeleteUserRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteUserCommand(request.Id), cancellationToken);
        return Unit.Value;
    }
}