using DrugStore.Application.Users.Commands.DeleteUserCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteUserRequest>
{
    public async Task<IResult> HandleAsync(DeleteUserRequest request, CancellationToken cancellationToken = default)
    {
        DeleteUserCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/users/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(User))
            .WithName("Delete User")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}