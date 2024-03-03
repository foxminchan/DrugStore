namespace DrugStore.Infrastructure.Merchant.Abstractions;

public sealed record ChargeRequest(
    string Currency,
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description
);