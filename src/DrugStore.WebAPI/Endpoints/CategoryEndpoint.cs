using Ardalis.Result;
using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Application.Categories.Commands.DeleteCategoryCommand;
using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Application.Categories.Queries.GetByIdQuery;
using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints;

public sealed class CategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/categories")
            .WithTags("Category")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("", GetCategories).WithName(nameof(GetCategories));
        group.MapGet("{id:guid}", GetCategoryById).WithName(nameof(GetCategoryById));
        group.MapPost("", CreateCategory).WithName(nameof(CreateCategory));
        group.MapPut("", UpdateCategory).WithName(nameof(UpdateCategory));
        group.MapDelete("{id:guid}", DeleteCategory).WithName(nameof(DeleteCategory));
    }

    private static async Task<Result<CategoryVm>> GetCategoryById(
        [FromServices] ISender sender,
        [FromRoute] CategoryId id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<Result<List<CategoryVm>>> GetCategories(
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(), cancellationToken);

    private static async Task<Result<CategoryId>> CreateCategory(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromBody] CategoryCreateRequest command,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateCategoryCommand(requestId, command), cancellationToken);

    private static async Task<Result<CategoryVm>> UpdateCategory(
        [FromServices] ISender sender,
        [FromBody] UpdateCategoryCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteCategory(
        [FromServices] ISender sender,
        [FromRoute] CategoryId id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteCategoryCommand(id), cancellationToken);
}