using DrugStore.Application.Users.Queries.GetByIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class GetById(ISender sender) : IEndpoint<IResult, GetUserByIdRequest>
{
    public async Task<IResult> HandleAsync(
        GetUserByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        GetByIdQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        var response = result.Value.Adapt<UserDto>();

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<UserDto>()
            .WithTags(nameof(User))
            .WithName("Get User By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit()
            .CacheOutput();
}