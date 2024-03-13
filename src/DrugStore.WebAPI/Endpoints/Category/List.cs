using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class List(ISender sender) : IEndpoint<ListCategoryResponse>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories", HandleAsync)
            .Produces<ListCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("List Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ListCategoryResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetListQuery(), cancellationToken);

        return new()
        {
            Categories = [.. result.Value.Select(x => new CategoryDto(x.Id, x.Name, x.Description))]
        };
    }
}