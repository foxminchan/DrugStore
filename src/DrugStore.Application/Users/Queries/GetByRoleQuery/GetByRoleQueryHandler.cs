using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Helpers;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Application.Users.Queries.GetByRoleQuery;

public sealed class GetByRoleQueryHandler(UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetByRoleQuery, PagedResult<List<UserVm>>>
{
    public async Task<PagedResult<List<UserVm>>> Handle(GetByRoleQuery request, CancellationToken cancellationToken)
    {
        var query = await userManager.GetUsersInRoleAsync(
            request.IsStaff ? RoleHelper.Admin : RoleHelper.Customer
        );

        query = [.. query.OrderBy(x => x.Id)];

        if (!request.Filter.IsAscending)
            query = [.. query.OrderByDescending(x => x.Id)];

        var customers = query
            .Skip((request.Filter.PageIndex - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .Select(x => new UserVm(x.Id, x.Email, x.FullName, x.PhoneNumber, x.Address))
            .ToList();

        if (!string.IsNullOrEmpty(request.Filter.Search))
            customers = customers.Where(x => x is { FullName: { }, Email: { } } &&
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
        return new(pagedInfo, customers);
    }
}