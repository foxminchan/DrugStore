using Ardalis.Result;
using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Application.Orders.Commands.DeleteOrderCommand;
using DrugStore.Application.Orders.Commands.UpdateOrderCommand;
using DrugStore.Application.Orders.Queries.GetByIdQuery;
using DrugStore.Application.Orders.Queries.GetListByUserIdQuery;
using DrugStore.Application.Orders.Queries.GetListQuery;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints;

public sealed class OrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/orders")
            .WithTags("Orders")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("{orderId}", GetOrder).WithName(nameof(GetOrder));
        group.MapGet("", GetOrders).WithName(nameof(GetOrders));
        group.MapGet("customer/{customerId}", GetOrdersByCustomer)
            .WithName(nameof(GetOrdersByCustomer));
        group.MapPost("", CreateOrder).WithName(nameof(CreateOrder));
        group.MapPut("", UpdateOrder).WithName(nameof(UpdateOrder));
        group.MapDelete("{orderId}", DeleteOrder).WithName(nameof(DeleteOrder));
    }

    private static async Task<Result<OrderVm>> GetOrder(
        [FromServices] ISender sender,
        [FromRoute] OrderId orderId,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(orderId), cancellationToken);

    private static async Task<PagedResult<List<OrderVm>>> GetOrders(
        [FromServices] ISender sender,
        [AsParameters] FilterHelper filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(filter), cancellationToken);

    private static async Task<PagedResult<List<OrderVm>>> GetOrdersByCustomer(
        [FromServices] ISender sender,
        [FromRoute] IdentityId customerId,
        [AsParameters] PagingHelper filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListByUserIdQuery(customerId, filter), cancellationToken);

    private static async Task<Result<OrderId>> CreateOrder(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromBody] OrderCreateRequest order,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateOrderCommand(requestId, order), cancellationToken);

    private static async Task<Result<OrderVm>> UpdateOrder(
        [FromServices] ISender sender,
        [FromBody] OrderUpdateRequest order,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateOrderCommand(order), cancellationToken);

    private static async Task<Result<Guid>> DeleteOrder(
        [FromServices] ISender sender,
        [FromRoute] OrderId orderId,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteOrderCommand(orderId), cancellationToken);
}