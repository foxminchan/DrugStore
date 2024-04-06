using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Application.Users.Queries.GetListQuery;

public sealed class GetListQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetListQuery, PagedResult<List<UserVm>>>
{
    public async Task<PagedResult<List<UserVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var query = userManager.Users.ToList();

        if (!string.IsNullOrEmpty(request.Role))
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(request.Role);
            query = [.. usersInRole.OrderBy(x => x.Id)];
        }

        if (request.Filter.IsAscending)
            query = [.. query.OrderBy(x => x.Id)];
        else
            query = [.. query.OrderByDescending(x => x.Id)];

        var users = query
            .Skip((request.Filter.PageIndex - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .Select(mapper.Map<UserVm>)
            .ToList();

        if (!string.IsNullOrEmpty(request.Filter.Search))
            users = users.AsEnumerable()
                .Where(x => x is { FullName: not null, Email: not null } &&
                            (
                                x.Email.Contains(request.Filter.Search) ||
                                x.FullName.Contains(request.Filter.Search)
                            )
                ).ToList();

        var totalRecords = await userManager.Users.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pagedInfo = new(request.Filter.PageIndex, request.Filter.PageSize, totalPages, totalRecords);
        return new(pagedInfo, users);
    }
}