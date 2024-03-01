namespace DrugStore.Infrastructure.Exception;

public sealed class InvalidIdempotencyException() : System.Exception("Invalid idempotency key.");