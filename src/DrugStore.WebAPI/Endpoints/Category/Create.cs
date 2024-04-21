using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.Exception;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateCategoryRequest>
{
    public async Task<IResult> HandleAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        CreateCategoryCommand command = new(requestId, request.Category.Name, request.Category.Description);

        var result = await sender.Send(command, cancellationToken);

        CreateCategoryResponse response = new(result.Value);

        return Results.Created($"/api/v1/categories/{response.Id}", response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/categories", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateCategoryPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Category))
            .WithName("Create Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}