using Ardalis.Result;
using DrugStore.Application.Categories.Commands.CreateCategoryPostCommand;
using DrugStore.Application.Categories.Commands.DeleteCategoryPostCommand;
using DrugStore.Application.Categories.Commands.UpdateCategoryPostCommand;
using DrugStore.Application.Categories.Queries.GetPostByIdQuery;
using DrugStore.Application.Categories.Queries.GetPostListQuery;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints;

public sealed class PostEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/posts")
            .WithTags("Post")
            .MapToApiVersion(new(1, 0));
        group.RequirePerUserRateLimit();
        group.MapGet("{categoryId:guid}", GetPosts).WithName(nameof(GetPosts));
        group.MapGet("{postId:guid}/categories/{categoryId:guid}", GetPostById).WithName(nameof(GetPostById));
        group.MapPost("", CreatePost).WithName(nameof(CreatePost));
        group.MapPut("", UpdatePost).WithName(nameof(UpdatePost));
        group.MapDelete("{postId:guid}/categories/{categoryId:guid}", DeletePost).WithName(nameof(DeletePost));
    }

    private static async Task<PagedResult<List<PostVm>>> GetPosts(
        [FromServices] ISender sender,
        [FromRoute] CategoryId categoryId,
        [AsParameters] BaseFilter filter,
        CancellationToken cancellationToken)
        => await sender.Send(new GetPostListQuery(categoryId, filter), cancellationToken);

    private static async Task<Result<PostVm>> GetPostById(
        [FromServices] ISender sender,
        [FromRoute] PostId postId,
        [FromRoute] CategoryId categoryId,
        CancellationToken cancellationToken)
        => await sender.Send(new GetPostByIdQuery(categoryId, postId), cancellationToken);

    private static async Task<Result<PostId>> CreatePost(
        [FromServices] ISender sender,
        [FromHeader(Name = "X-Idempotency-Key")]
        string idempotencyKey,
        [FromForm] IFormFile? imageFile,
        [FromForm] PostCreateRequest command,
        CancellationToken cancellationToken)
        => !Guid.TryParse(idempotencyKey, out var requestId)
            ? throw new InvalidIdempotencyException()
            : await sender.Send(new CreateCategoryPostCommand(requestId, command, imageFile), cancellationToken);

    private static async Task<Result<PostVm>> UpdatePost(
        [FromServices] ISender sender,
        [FromForm] IFormFile? imageFile,
        [FromForm] PostUpdateRequest command,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateCategoryPostCommand(command, imageFile), cancellationToken);

    private static async Task<Result> DeletePost(
        [FromServices] ISender sender,
        [FromRoute] CategoryId categoryId,
        [FromRoute] PostId postId,
        CancellationToken cancellationToken)
        => await sender.Send(new DeleteCategoryPostCommand(categoryId, postId), cancellationToken);
}