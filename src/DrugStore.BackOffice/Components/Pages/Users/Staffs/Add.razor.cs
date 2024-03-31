using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;

namespace DrugStore.BackOffice.Components.Pages.Users.Staffs;

public sealed partial class Add
{
    private bool _busy;

    private bool _error;

    private bool _isDefaultPassword;

    private readonly CreateUser _user = new();

    private const string DEFAULT_PASSWORD = "P@ssw0rd";

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private ILogger<Add> Logger { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private async Task OnSubmit(CreateUser customer)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;
                customer.IsAdmin = true;

                Logger.LogInformation("[{Page}] Customer information: {Requets}", nameof(Add),
                    JsonSerializer.Serialize(customer));

                await UserApi.CreateUserAsync(customer, Guid.NewGuid());
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Customer created successfully"
                });
                NavigationManager.NavigateTo("/users");
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to create Customer" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true)
            NavigationManager.NavigateTo("/users");
    }

    private void SetDefaultPassword(bool isDefaultPassword)
    {
        _isDefaultPassword = isDefaultPassword;
        _user.Password = DEFAULT_PASSWORD;
        _user.ConfirmPassword = DEFAULT_PASSWORD;
    }
}