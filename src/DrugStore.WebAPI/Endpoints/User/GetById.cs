using DrugStore.Application.Users.Queries.GetByIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class GetById(ISender sender) : IEndpoint<UserDto, GetUserByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .WithTags(nameof(User))
            .WithName("Get User By Id")
            .Produces<UserDto>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit()
            .CacheOutput();

    public async Task<UserDto> HandleAsync(
        GetUserByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetByIdQuery(request.Id), cancellationToken);
        var user = result.Value;
        return new(user.Id, user.Email, user.FullName, user.Phone, user.Address);
    }
}