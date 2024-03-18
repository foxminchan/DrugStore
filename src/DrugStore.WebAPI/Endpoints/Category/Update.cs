using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Update(ISender sender) : IEndpoint<UpdateCategoryResponse, UpdateCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/categories", async (UpdateCategoryRequest request) => await HandleAsync(request))
            .Produces<UpdateCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("Update Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateCategoryResponse> HandleAsync(
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(request.Adapt<UpdateCategoryCommand>(), cancellationToken);
        return new(result.Value.Adapt<CategoryDto>());
    }
}