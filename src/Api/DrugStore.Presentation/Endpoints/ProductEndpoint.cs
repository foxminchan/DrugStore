using Ardalis.Result;
using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.Application.Products.Queries.GetByIdQuery;
using DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;
using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.SharedKernel;
using DrugStore.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Endpoints;

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
        group.MapGet("{id:guid}", GetProductById).WithName(nameof(GetProductById));
        group.MapGet("/category/{id:guid}", GetProductsByCategoryId).WithName(nameof(GetProductsByCategoryId));
        group.MapPost("", CreateProduct).WithName(nameof(CreateProduct));
        group.MapPut("", UpdateProduct).WithName(nameof(UpdateProduct));
        group.MapDelete("{id:guid}", DeleteProduct).WithName(nameof(DeleteProduct));
    }

    private static async Task<Result<ProductVm>> GetProductById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<Result<IEnumerable<ProductVm>>> GetProducts(
        [FromServices] ISender sender,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(filter), cancellationToken);

    private static async Task<Result<IEnumerable<ProductVm>>> GetProductsByCategoryId(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListByCategoryIdQuery(id, filter), cancellationToken);

    private static async Task<Result<Guid>> CreateProduct(
        [FromServices] ISender sender,
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result<ProductVm>> UpdateProduct(
        [FromServices] ISender sender,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteProduct(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteProductCommand(id), cancellationToken);
}
