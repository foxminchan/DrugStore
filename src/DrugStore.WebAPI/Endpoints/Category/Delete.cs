using DrugStore.Application.Categories.Commands.DeleteCategoryCommand;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces<Unit>()
            .WithTags(nameof(Category))
            .WithName("Delete Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<Unit> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteCategoryCommand(request.Id), cancellationToken);
        return Unit.Value;
    }
}