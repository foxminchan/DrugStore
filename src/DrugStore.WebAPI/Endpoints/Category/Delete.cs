using DrugStore.Application.Categories.Commands.DeleteCategoryCommand;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Category))
            .WithName("Delete Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken = default)
    {
        DeleteCategoryCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}