using DrugStore.BackOffice.Components.Pages.Orders.Requets;
using DrugStore.BackOffice.Components.Pages.Orders.Response;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Orders.Services;

public interface IOrdersApi
{
    [Get("/orders")]
    Task<ListOrder> ListOrdersAsync([Query] FilterHelper request);

    [Get("/orders/{orderId}")]
    Task<OrderDetail> GetOrderAsync(Guid orderId);

    [Post("/orders")]
    Task CreateOrderAsync(CreateOrder request, [Header("X-Idempotency-Key")] Guid key);

    [Delete("/orders/{orderId}")]
    Task DeleteOrderAsync(Guid orderId);
}