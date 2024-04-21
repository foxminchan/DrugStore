using DrugStore.Application.Users.Commands.CreateUserCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.Exception;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateUserRequest>
{
    public async Task<IResult> HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        CreateUserCommand command = new(
            requestId,
            request.User.Email,
            request.User.Password,
            request.User.ConfirmPassword,
            request.User.FullName,
            request.User.Phone,
            request.User.Address,
            request.User.IsAdmin
        );

        var result = await sender.Send(command, cancellationToken);

        CreateUserResponse response = new(result.Value);

        return Results.Created($"/api/v1/users/{response.Id}", response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/users", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateUserPayload payload) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateUserResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(User))
            .WithName("Create User")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}