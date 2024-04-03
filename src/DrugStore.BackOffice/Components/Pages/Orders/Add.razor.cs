using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Orders.Requets;
using DrugStore.BackOffice.Components.Pages.Orders.Services;
using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Response;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Orders;

public sealed partial class Add
{
    private bool _busy;

    private bool _error;

    private int _productCount;

    private int _customerCount;

    private List<User> _users = [];

    private List<Product> _products = [];

    private readonly CreateOrder _order = new();

    private readonly List<OrderItemPayload> _items = [];

    private RadzenDataGrid<OrderItemPayload> _dataGrid = default!;

    [Inject] private ILogger<Add> Logger { get; set; } = default!;

    [Inject] private IUserApi UserApi { get; set; } = default!;

    [Inject] private IOrdersApi OrdersApi { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            var users = await UserApi.ListUsersAsync(new(), null);
            _users = users.Users;
            _customerCount = _users.Count;

            var products = await ProductsApi.ListProductsAsync(new());
            _products = products.Products;
            _productCount = _products.Count;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Data" });
            NavigationManager.NavigateTo("/orders");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(CreateOrder order)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;

                order.Items = _items;

                Logger.LogInformation("[{Page}] Order information: {Order}", nameof(Add),
                    JsonSerializer.Serialize(order));

                await OrdersApi.CreateOrderAsync(order, Guid.NewGuid());
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Order created successfully"
                });
                NavigationManager.NavigateTo("/orders");
            }
        }
        catch (Exception e)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to create Order" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task UpdateItem(OrderItemPayload order)
    {
        _items.Add(order);
        await _dataGrid.UpdateRow(order);
    }

    private async Task AddItem()
    {
        OrderItemPayload item = new();
        _items.Add(item);
        await _dataGrid.InsertRow(item);
    }

    private async Task DeleteItem(OrderItemPayload order)
    {
        _items.Remove(order);
        await _dataGrid.Reload();
    }

    private async Task SaveRow(OrderItemPayload item) => await _dataGrid.UpdateRow(item);

    private void CancelEdit(OrderItemPayload item)
    {
        _items.Remove(item);
        _dataGrid.CancelEditRow(item);
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true)
            NavigationManager.NavigateTo("/orders");
    }
}