namespace DrugStore.Infrastructure.Merchant.Abstractions;

public sealed record CustomerResponse(
    string Id,
    string Email,
    string Name
);