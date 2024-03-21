namespace DrugStore.Infrastructure.Exception;

public sealed class DatabaseInitializationException(string message, System.Exception exception)
    : System.Exception(message, exception);