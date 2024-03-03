namespace DrugStore.Infrastructure.Merchant.Abstractions;

public sealed record CustomerRequest(
    string Email,
    string Name,
    CardRequest Card
);