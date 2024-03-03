namespace DrugStore.Infrastructure.Merchant.Abstractions;

public sealed record ChargeResponse(
    string ChargeId,
    string Currency,
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description
);