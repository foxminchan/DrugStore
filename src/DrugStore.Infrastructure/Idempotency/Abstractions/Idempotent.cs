namespace DrugStore.Infrastructure.Idempotency.Abstractions;

public sealed class Idempotent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}