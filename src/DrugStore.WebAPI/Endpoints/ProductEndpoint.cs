using Ardalis.Result;
using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.Application.Products.Commands.UpdateProductImageCommand;
using DrugStore.Application.Products.Queries.GetByIdQuery;
using DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;
using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints;

public sealed class ProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/products")
            .WithTags("Product")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("", GetProducts).WithName(nameof(GetProducts));
        group.MapGet("{id}", GetProductById).WithName(nameof(GetProductById));
        group.MapGet("/category/{id}", GetProductsByCategoryId).WithName(nameof(GetProductsByCategoryId));
        group.MapPost("", CreateProduct).WithName(nameof(CreateProduct));
        group.MapPut("", UpdateProduct).WithName(nameof(UpdateProduct));
        group.MapPut("/images/{id}", UpdateImages).WithName(nameof(UpdateImages)).DisableAntiforgery();
        group.MapDelete("{id}", DeleteProduct).WithName(nameof(DeleteProduct));
    }

    private static async Task<Result<ProductVm>> GetProductById(
        [FromServices] ISender sender,
        [FromRoute] ProductId id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<PagedResult<List<ProductVm>>> GetProducts(
        [FromServices] ISender sender,
        [AsParameters] FilterHelper filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(filter), cancellationToken);

    private static async Task<Result<List<ProductVm>>> GetProductsByCategoryId(
        [FromServices] ISender sender,
        [FromRoute] CategoryId id,
        [AsParameters] PagingHelper filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListByCategoryIdQuery(id, filter), cancellationToken);

    private static async Task<Result<ProductId>> CreateProduct(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromBody] ProductCreateRequest command,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateProductCommand(requestId, command), cancellationToken);

    private static async Task<Result<ProductVm>> UpdateProduct(
        [FromServices] ISender sender,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result<ProductId>> UpdateImages(
        [FromServices] ISender sender,
        [FromRoute] ProductId id,
        [FromForm] IFormFile images,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateProductImageCommand(id, images), cancellationToken);

    private static async Task<Result> DeleteProduct(
        [FromServices] ISender sender,
        [FromRoute] ProductId id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteProductCommand(id), cancellationToken);
}