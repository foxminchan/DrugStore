using DrugStore.Application.Users.Commands.ResetPasswordCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ResetPassword(ISender sender) : IEndpoint<IResult, ResetPasswordRequest>
{
    public async Task<IResult> HandleAsync(ResetPasswordRequest request,
        CancellationToken cancellationToken = default)
    {
        ResetPasswordCommand command = new(request.Id);

        var result = await sender.Send(command, cancellationToken);

        ResetPasswordResponse response = new(result.Value);

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/reset-password/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<ResetPasswordResponse>()
            .WithTags(nameof(User))
            .WithName("Reset Password")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}