using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Users.Profile;

public partial class Index
{
    private readonly UpdateUser _user = new();
    private bool _busy;

    private bool _error;

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private ILogger<Index> Logger { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            _error = false;

            var cookie = HttpContextAccessor.HttpContext?.Request.Cookies[".AspNetCore.Identity.Application"];
            Logger.LogInformation("[{Page}] Cookie: {Cookie}", nameof(Index), cookie);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(cookie);
            var userId = token.Claims.First(claim => claim.Type == "sub").Value;
            var user = await UserApi.GetUserAsync(new(userId));

            _user.Id = user.Id.ToString();
            _user.FullName = user.FullName;
            _user.Email = user.Email;
            _user.Phone = user.Phone;
            _user.Address = new()
            {
                Province = user.Address.Province,
                City = user.Address.City,
                Street = user.Address.Street
            };
        }
        catch (Exception e)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load User" });
            NavigationManager.NavigateTo("~/");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateUser user)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;

                Logger.LogInformation("[{Page}] User information: {Request}", nameof(Index),
                    JsonSerializer.Serialize(user));

                if (string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.ConfirmPassword) ||
                    string.IsNullOrEmpty(user.OldPassword))
                {
                    UpdateUserInfo updateUserInfo = new()
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Phone = user.Phone,
                        Address = user.Address
                    };
                    await UserApi.UpdateUserInfoAsync(updateUserInfo);
                }
                else
                {
                    await UserApi.UpdateUserAsync(user);
                }

                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "User updated successfully"
                });
                NavigationManager.NavigateTo("~/");
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to update User" });
        }
        finally
        {
            _busy = false;
        }
    }
}