using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Response;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Services;

public interface IUserApi
{
    [Get("/users")]
    Task<ListUser> ListUsersAsync([Query] FilterHelper filter, [Query] string? role);

    [Get("/users/{id}")]
    Task<User> GetUserAsync(Guid id);

    [Get("/users/reset-password/{id}")]
    Task ResetPasswordAsync(Guid id);

    [Put("/users")]
    Task UpdateUserAsync(UpdateUser user);

    [Put("/users/info")]
    Task UpdateUserInfoAsync(UpdateUserInfo user);

    [Post("/users")]
    Task CreateUserAsync(CreateUser user, [Header("X-Idempotency-Key")] Guid key);

    [Delete("/users/{id}")]
    Task DeleteUserAsync(Guid id);
}