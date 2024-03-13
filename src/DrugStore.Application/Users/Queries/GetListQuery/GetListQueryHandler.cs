using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Application.Users.Queries.GetListQuery;

public sealed class GetListQueryHandler(UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetListQuery, PagedResult<List<UserVm>>>
{
    public async Task<PagedResult<List<UserVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var query = userManager.Users.OrderBy(x => x.Id);

        if (!request.Filter.IsAscending) query = query.OrderByDescending(x => x.Id);

        var users = await query
            .Skip((request.Filter.PageIndex - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .Select(x => new UserVm(x.Id, x.Email, x.FullName, x.PhoneNumber, x.Address))
            .ToListAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.Filter.Search))
            users = users.AsEnumerable()
                .Where(x => x is { FullName: { }, Email: { } } &&
                            (
                                x.Email.Contains(request.Filter.Search) ||
                                x.FullName.Contains(request.Filter.Search)
                            )
                ).ToList();

        var totalRecords = await userManager.Users.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pagedInfo = new(
            request.Filter.PageIndex,
            request.Filter.PageSize,
            totalPages,
            totalRecords
        );
        return new(pagedInfo, users);
    }
}