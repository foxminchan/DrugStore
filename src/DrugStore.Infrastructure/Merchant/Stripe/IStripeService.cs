using DrugStore.Infrastructure.Merchant.Abstractions;

namespace DrugStore.Infrastructure.Merchant.Stripe;

public interface IStripeService
{
    Task<CustomerResponse> CreateCustomer(CustomerRequest request, CancellationToken cancellationToken);
    Task<ChargeResponse> CreateCharge(ChargeRequest request, CancellationToken cancellationToken);
}