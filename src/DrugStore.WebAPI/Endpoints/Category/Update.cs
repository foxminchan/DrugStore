using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateCategoryRequest>
{
    public async Task<IResult> HandleAsync(
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.Adapt<UpdateCategoryCommand>();

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateCategoryResponse
        {
            Category = result.Value.Adapt<CategoryDto>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/categories", async (UpdateCategoryRequest request) => await HandleAsync(request))
            .Produces<UpdateCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("Update Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}