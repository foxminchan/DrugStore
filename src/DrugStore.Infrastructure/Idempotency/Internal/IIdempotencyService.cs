namespace DrugStore.Infrastructure.Idempotency.Internal;

public interface IIdempotencyService
{
    bool RequestExists(Guid id);
    void CreateRequestForCommand(Guid id, string name);
}