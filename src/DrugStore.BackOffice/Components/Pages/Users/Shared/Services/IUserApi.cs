﻿using DrugStore.BackOffice.Components.Pages.Users.Shared.Response;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Services;

public interface IUserApi
{
    [Get("/users")]
    Task<ListUser> ListUsersAsync([Query] FilterHelper filter, [Query] string? role);

    [Delete("/users/{id}")]
    Task DeleteUserAsync(Guid id);
}