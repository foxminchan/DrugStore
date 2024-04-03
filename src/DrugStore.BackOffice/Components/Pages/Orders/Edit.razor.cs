using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Orders.Requets;
using DrugStore.BackOffice.Components.Pages.Orders.Services;
using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Orders;

public sealed partial class Edit
{
    private bool _busy;

    private bool _error;

    private int _productCount;

    private List<Product> _products = [];

    private readonly UpdateOrder _order = new();

    private readonly List<OrderItemPayload> _items = [];

    private RadzenDataGrid<OrderItemPayload> _dataGrid = default!;

    [Inject] private ILogger<Edit> Logger { get; set; } = default!;

    [Inject] private IOrdersApi OrdersApi { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

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

            var products = await ProductsApi.ListProductsAsync(new());
            _products = products.Products;
            _productCount = _products.Count;

            var order = await OrdersApi.GetOrderAsync(Guid.Parse(Id));
            _order.Id = order.Order.Id.ToString();
            _order.Code = order.Order.Code;
            _order.CustomerId = order.Order.CustomerId.ToString();
            _order.Items = order.Items
                .Select(x => new OrderItemPayload
                {
                    Id = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price
                })
                .ToList();
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load data" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateOrder order)
    {
        try
        {
            _busy = true;
            _error = false;

            Logger.LogInformation("[{Page}] Order information: {Requets}", nameof(Edit),
                JsonSerializer.Serialize(order));

            await OrdersApi.UpdateOrderAsync(order);
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Order updated successfully" });
            NavigationManager.NavigateTo("/orders");
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to update order" });
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