using DrugStore.BackOffice.Components.Pages.Users.Shared.Response;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Users.Customers;

public sealed partial class Index
{
    private int _count;

    private List<User> _customers = [];

    private RadzenDataGrid<User> _dataGrid = default!;

    private bool _error;

    private bool _loading;

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await UserApi.ListUsersAsync(new(), "Customer");
            _customers = result.Users;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Customer" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task DeleteCustomer(Guid id)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.DELETE_ITEM) == true)
            {
                await UserApi.DeleteUserAsync(id);
                _customers.RemoveAll(x => x.Id == id);
                _count = _customers.Count;
                await _dataGrid.Reload();
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Customer deleted successfully."
                });
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to delete Customer" });
        }
    }

    private async Task Search(ChangeEventArgs args)
    {
        try
        {
            _loading = true;
            var result = await UserApi.ListUsersAsync(new() { Search = args.Value?.ToString() }, "Customer");
            _customers = result.Users;
            await _dataGrid.GoToPage(0);
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Customer" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task AddCustomers() => NavigationManager.NavigateTo("/customers/add");

    private async Task EditCustomer(Guid id) => NavigationManager.NavigateTo($"/customers/edit/{id}");

    private async Task ExportClick() => NavigationManager.NavigateTo("/export/users/Customer", true);
}