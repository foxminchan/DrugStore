namespace DrugStore.Infrastructure.Idempotency.Internal;

public interface IIdempotencyService
{
    public bool RequestExists(Guid id);
    public void CreateRequestForCommand(Guid id, string name);
}
