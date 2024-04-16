using DrugStore.BackOffice.Components.Pages.Orders.Response;
using DrugStore.BackOffice.Components.Pages.Orders.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Orders;

public sealed partial class Index
{
    private int _count;

    private RadzenDataGrid<Order> _dataGrid = default!;

    private bool _error;

    private bool _loading;

    private List<Order> _orders = [];

    [Inject] private IOrdersApi OrdersApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await OrdersApi.ListOrdersAsync(new());
            _orders = result.Orders;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        catch (Exception e)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Order" });
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
            _loading = true;
            var result = await OrdersApi.ListOrdersAsync(new() { Search = args.Value?.ToString() });
            _orders = result.Orders;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to search Order" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task DeleteOrder(Guid orderId)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.DELETE_ITEM) == true)
            {
                await OrdersApi.DeleteOrderAsync(orderId);
                await _dataGrid.Reload();
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Order deleted successfully"
                });
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "Unable to delete Order"
            });
        }
    }

    private async Task AddOrder() => NavigationManager.NavigateTo("/orders/add");

    private async Task ExportClick() => NavigationManager.NavigateTo("/export/orders", true);

    private async Task EditOrder(Guid orderId) => NavigationManager.NavigateTo($"/orders/edit/{orderId}");
}