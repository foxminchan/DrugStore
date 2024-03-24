using DrugStore.Application.Users.Commands.CreateUserCommand;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Create(ISender sender) : IEndpoint<CreateUserResponse, CreateUserRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/users", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateUserPayload payload) => await HandleAsync(new(idempotencyKey, payload)))
            .WithTags(nameof(User))
            .WithName("Create User")
            .Produces<CreateUserResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<CreateUserResponse> HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateUserCommand(
                requestId,
                request.User.Email,
                request.User.Password,
                request.User.ConfirmPassword,
                request.User.FullName,
                request.User.Phone,
                request.User.Address,
                request.User.IsAdmin
            ), cancellationToken);

        return new(result.Value);
    }
}