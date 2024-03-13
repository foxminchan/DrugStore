using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Domain.SharedKernel;
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
            .RequireAuthorization();

    public async Task<UpdateCategoryResponse> HandleAsync(
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new UpdateCategoryCommand(request.Id, request.Name, request.Description), cancellationToken
        );
        return new(new(result.Value.Id, result.Value.Name, result.Value.Description));
    }
}