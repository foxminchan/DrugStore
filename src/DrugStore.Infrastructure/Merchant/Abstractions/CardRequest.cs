namespace DrugStore.Infrastructure.Merchant.Abstractions;

public sealed record CardRequest(
    string Name,
    string Number,
    string ExpiryYear,
    string ExpiryMonth,
    string Cvc
);