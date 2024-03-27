using DrugStore.BackOffice.Components.Pages.Users.Shared.Response;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Users.Staffs;

public sealed partial class Index
{
    private int _count;

    private bool _error;

    private bool _loading;

    private List<User> _users = [];

    private RadzenDataGrid<User> _dataGrid = default!;

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await UserApi.ListUsersAsync(new(), "Admin");
            _users = result.Users;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Users" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task Search(ChangeEventArgs args)
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await UserApi.ListUsersAsync(new() { Search = args.Value?.ToString() }, "Admin");
            _users = result.Users;
            _count = (int)result.PagedInfo.TotalRecords;
            await _dataGrid.GoToPage(0);
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to search Users" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task AddUser() => NavigationManager.NavigateTo("/users/add");

    private async Task ExportClick() => NavigationManager.NavigateTo("/export/users/Admin", true);
}