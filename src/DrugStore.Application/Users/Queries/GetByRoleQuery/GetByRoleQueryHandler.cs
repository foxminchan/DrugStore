﻿using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Application.Users.Queries.GetByRoleQuery;

public sealed class GetByRoleQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetByRoleQuery, PagedResult<List<UserVm>>>
{
    public async Task<PagedResult<List<UserVm>>> Handle(GetByRoleQuery request, CancellationToken cancellationToken)
    {
        var query = await userManager.GetUsersInRoleAsync(
            request.IsStaff ? Roles.Admin : Roles.Customer
        );

        query = [.. query.OrderBy(x => x.Id)];

        if (!request.Filter.IsAscending)
            query = [.. query.OrderByDescending(x => x.Id)];

        var customers = query
            .Skip((request.Filter.PageIndex - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .Select(mapper.Map<UserVm>)
            .ToList();

        if (!string.IsNullOrEmpty(request.Filter.Search))
            customers = customers.Where(x => x is { FullName: not null, Email: not null } &&
                                             (
                                                 x.Email.Contains(request.Filter.Search) ||
                                                 x.FullName.Contains(request.Filter.Search)
                                             )
            ).ToList();

        var totalRecords = await userManager.Users.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pagedInfo = new(request.Filter.PageIndex, request.Filter.PageSize, totalPages, totalRecords);
        return new(pagedInfo, customers);
    }
}