﻿using Ardalis.Result;

using DrugStore.Application.Categories.Commands.CreateCategoryNewsCommand;
using DrugStore.Application.Categories.Commands.DeleteCategoryNewsCommand;
using DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;
using DrugStore.Application.Categories.Queries.GetNewsByIdQuery;
using DrugStore.Application.Categories.Queries.GetNewsListQuery;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Presentation.Extensions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Endpoints;

public sealed class NewsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/news")
            .WithTags("Category News")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("{categoryId:guid}", GetNews).WithName(nameof(GetNews));
        group.MapGet("{newsId:guid}/categories/{categoryId:guid}", GetNewsById).WithName(nameof(GetNewsById));
        group.MapPost("", CreateNews).WithName(nameof(CreateNews));
        group.MapPut("", UpdateNews).WithName(nameof(UpdateNews));
        group.MapDelete("{newsId:guid}/categories/{categoryId:guid}", DeleteNews).WithName(nameof(DeleteNews));
    }

    private static async Task<PagedResult<List<NewsVm>>> GetNews(
        [FromServices] ISender sender,
        [FromRoute] Guid categoryId,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetNewsListQuery(categoryId, filter), cancellationToken);

    private static async Task<Result<NewsVm>> GetNewsById(
        [FromServices] ISender sender,
        [FromRoute] Guid newsId,
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken)
        => await sender.Send(new GetNewsByIdQuery(categoryId, newsId), cancellationToken);

    private static async Task<Result<Guid>> CreateNews(
        [FromServices] ISender sender,
        [FromBody] CreateCategoryNewsCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result<NewsVm>> UpdateNews(
        [FromServices] ISender sender,
        [FromBody] UpdateCategoryNewsCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteNews(
        [FromServices] ISender sender,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid newsId,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteCategoryNewsCommand(categoryId, newsId), cancellationToken);
}
