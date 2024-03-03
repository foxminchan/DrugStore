using DrugStore.Infrastructure.Merchant.Abstractions;
using Stripe;

namespace DrugStore.Infrastructure.Merchant.Stripe.Internal;

public sealed class StripeService(
    TokenService tokenService,
    CustomerService customerService,
    ChargeService chargeService) : IStripeService
{
    public async Task<CustomerResponse> CreateCustomer(CustomerRequest request, CancellationToken cancellationToken)
    {
        var tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = request.Card.Name,
                Number = request.Card.Number,
                ExpYear = request.Card.ExpiryYear,
                ExpMonth = request.Card.ExpiryMonth,
                Cvc = request.Card.Cvc
            }
        };

        var token = await tokenService.CreateAsync(tokenOptions, null, cancellationToken);

        var customerOptions = new CustomerCreateOptions
        {
            Email = request.Email,
            Name = request.Name,
            Source = token.Id
        };

        var customer = await customerService.CreateAsync(customerOptions, null, cancellationToken);

        return new(customer.Id, customer.Email, customer.Name);
    }

    public async Task<ChargeResponse> CreateCharge(ChargeRequest request, CancellationToken cancellationToken)
    {
        var chargeOptions = new ChargeCreateOptions
        {
            Currency = request.Currency,
            Amount = request.Amount,
            ReceiptEmail = request.ReceiptEmail,
            Customer = request.CustomerId,
            Description = request.Description
        };

        var charge = await chargeService.CreateAsync(chargeOptions, null, cancellationToken);

        return new(
            charge.Id,
            charge.Currency,
            charge.Amount,
            charge.CustomerId,
            charge.ReceiptEmail,
            charge.Description
        );
    }
}