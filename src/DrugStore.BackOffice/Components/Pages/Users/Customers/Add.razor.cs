using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Constants;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Users.Customers;

public sealed partial class Add
{
    private bool _busy;

    private bool _error;

    private bool _isDefaultPassword;

    private readonly CreateUser _customer = new();

    [Inject] private IUserApi UserApi { get; set; } = default!;

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
                await UserApi.CreateUserAsync(customer, Guid.NewGuid());
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Customer created successfully"
                });
                NavigationManager.NavigateTo("/customers");
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
            NavigationManager.NavigateTo("/customers");
    }

    private void SetDefaultPassword(bool isDefaultPassword)
    {
        _isDefaultPassword = isDefaultPassword;
        _customer.Password = "P@ssw0rd";
        _customer.ConfirmPassword = "P@ssw0rd";
    }
}