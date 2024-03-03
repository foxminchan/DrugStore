namespace DrugStore.Infrastructure.Email.Abstractions;

public sealed record EmailMetadata(
    object? Model,
    string? Subject,
    string? Template,
    string? To
);