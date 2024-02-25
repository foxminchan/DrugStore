using Ardalis.Result;
using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Application.Categories.Commands.DeleteCategoryCommand;
using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Application.Categories.Queries.GetByIdQuery;
using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.Application.Categories.ViewModel;
using DrugStore.Domain.SharedKernel;
using DrugStore.Presentation.Extensions;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Endpoints;

public sealed class CategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/categories")
            .WithTags("Category")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("", GetAll).WithName(nameof(GetAll));
        group.MapGet("{id:guid}", GetById).WithName(nameof(GetById));
        group.MapPost("", Create).WithName(nameof(Create));
        group.MapPut("", Update).WithName(nameof(Update));
        group.MapDelete("{id:guid}", Delete).WithName(nameof(Delete));
    }

    private static async Task<Result<CategoryVm>> GetById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<Result<IEnumerable<CategoryVm>>> GetAll(
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(), cancellationToken);

    private static async Task<Result<Guid>> Create(
        [FromServices] ISender sender,
        [FromBody] CreateCategoryCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result<CategoryVm>> Update(
        [FromServices] ISender sender,
        [FromBody] UpdateCategoryCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> Delete(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteCategoryCommand(id), cancellationToken);
}
