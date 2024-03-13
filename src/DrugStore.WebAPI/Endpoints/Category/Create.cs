using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Exception;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Create(ISender sender) : IEndpoint<CreateCategoryResponse, CreateCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/categories", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateCategoryPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("Create Category")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();

    public async Task<CreateCategoryResponse> HandleAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateCategoryCommand(
                requestId,
                request.Category.Name,
                request.Category.Description
            ), cancellationToken);

        return new(result.Value);
    }
}