using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Users.Customers;

public sealed partial class Edit
{
    private bool _busy;

    private bool _error;

    private readonly UpdateUserInfo _customer = new();

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Parameter] public required string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            _error = false;
            var customer = await UserApi.GetUserAsync(new(Id));
            _customer.Id = customer.Id.ToString();
            _customer.FullName = customer.FullName;
            _customer.Email = customer.Email;
            _customer.Phone = customer.Phone;
            _customer.Address = new()
            {
                Province = customer.Address.Province,
                City = customer.Address.City,
                Street = customer.Address.Street
            };
            _customer.Role = "Customer";
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Customer" });
            NavigationManager.NavigateTo("/customers");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateUserInfo customer)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;
                await UserApi.UpdateUserInfoAsync(customer);
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Customer updated successfully"
                });
                NavigationManager.NavigateTo("/customers");
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to update Customer" });
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true) NavigationManager.NavigateTo("/customers");
    }
}