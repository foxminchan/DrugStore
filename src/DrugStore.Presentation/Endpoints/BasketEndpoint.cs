using Ardalis.Result;
using DrugStore.Application.Baskets.Commands.CreateBasketCommand;
using DrugStore.Application.Baskets.Commands.DeleteBasketCommand;
using DrugStore.Application.Baskets.Commands.UpdateBasketCommand;
using DrugStore.Application.Baskets.Queries.GetByUserId;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Endpoints;

public sealed class BasketEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/baskets")
            .WithTags("Baskets")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("{customerId:guid}", GetBasketByCustomerId).WithName(nameof(GetBasketByCustomerId));
        group.MapPost("", CreateBasket).WithName(nameof(CreateBasket));
        group.MapPut("", UpdateBasket).WithName(nameof(UpdateBasket));
        group.MapDelete("{customerId:guid}", DeleteBasket).WithName(nameof(DeleteBasket));
    }

    private static async Task<Result<BasketVm>> GetBasketByCustomerId(
        [FromServices] ISender sender,
        [FromRoute] Guid customerId,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByUserId(customerId, filter), cancellationToken);

    private static async Task<Result<Guid>> CreateBasket(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromBody] BasketCreateRequest command,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateBasketCommand(requestId, command), cancellationToken);

    private static async Task<Result<CustomerBasket>> UpdateBasket(
        [FromServices] ISender sender,
        [FromBody] UpdateBasketCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteBasket(
        [FromServices] ISender sender,
        [FromRoute] Guid customerId,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteBasketCommand(customerId), cancellationToken);
}