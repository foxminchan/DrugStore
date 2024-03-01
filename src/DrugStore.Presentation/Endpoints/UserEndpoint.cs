using Ardalis.Result;
using DrugStore.Application.Users.Commands.CreateUserCommand;
using DrugStore.Application.Users.Commands.DeleteUserCommand;
using DrugStore.Application.Users.Commands.UpdateUserCommand;
using DrugStore.Application.Users.Queries.GetByIdQuery;
using DrugStore.Application.Users.Queries.GetListQuery;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Endpoints;

public sealed class UserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/users")
            .WithTags("User")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("", GetUsers).WithName(nameof(GetUsers));
        group.MapGet("{id:guid}", GetUserById).WithName(nameof(GetUserById));
        group.MapPost("", CreateUser).WithName(nameof(CreateUser));
        group.MapPut("", UpdateUser).WithName(nameof(UpdateUser));
        group.MapDelete("{id:guid}", DeleteUser).WithName(nameof(DeleteUser));
    }

    private static async Task<Result<UserVm>> GetUserById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<PagedResult<List<UserVm>>> GetUsers(
        [FromServices] ISender sender,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(filter), cancellationToken);

    private static async Task<Result<Guid>> CreateUser(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromBody] UserCreateRequest command,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateUserCommand(requestId, command), cancellationToken);

    private static async Task<Result<UserVm>> UpdateUser(
        [FromServices] ISender sender,
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteUser(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteUserCommand(id), cancellationToken);
}