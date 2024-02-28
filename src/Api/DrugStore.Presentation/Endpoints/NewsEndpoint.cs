using Ardalis.Result;
using DrugStore.Application.News.Commands.CreateNewsCommand;
using DrugStore.Application.News.Commands.DeleteNewsCommand;
using DrugStore.Application.News.Commands.UpdateNewsCommand;
using DrugStore.Application.News.Queries.GetByIdQuery;
using DrugStore.Application.News.Queries.GetListQuery;
using DrugStore.Application.News.ViewModel;
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
            .WithTags("News")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("", GetNews).WithName(nameof(GetNews));
        group.MapGet("{id:guid}", GetNewsById).WithName(nameof(GetNewsById));
        group.MapPost("", CreateNews).WithName(nameof(CreateNews));
        group.MapPut("", UpdateNews).WithName(nameof(UpdateNews));
        group.MapDelete("{id:guid}", DeleteNews).WithName(nameof(DeleteNews));
    }

    private static async Task<Result<NewsVm>> GetNewsById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new GetByIdQuery(id), cancellationToken);

    private static async Task<PagedResult<List<NewsVm>>> GetNews(
        [FromServices] ISender sender,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetListQuery(filter), cancellationToken);

    private static async Task<Result<Guid>> CreateNews(
        [FromServices] ISender sender,
        [FromBody] CreateNewsCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result<NewsVm>> UpdateNews(
        [FromServices] ISender sender,
        [FromBody] UpdateNewsCommand command,
        CancellationToken cancellationToken)
        => await sender.Send(command, cancellationToken);

    private static async Task<Result> DeleteNews(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteNewsCommand(id), cancellationToken);
}
