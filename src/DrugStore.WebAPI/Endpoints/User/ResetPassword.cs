using DrugStore.Application.Users.Commands.ResetPasswordCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ResetPassword(ISender sender) : IEndpoint<ResetPasswordResponse, ResetPasswordRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/reset-password/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<ResetPasswordResponse>()
            .WithTags(nameof(User))
            .WithName("Reset Password")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ResetPasswordResponse> HandleAsync(ResetPasswordRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new ResetPasswordCommand(request.Id), cancellationToken);
        return new(result.Value);
    }
}