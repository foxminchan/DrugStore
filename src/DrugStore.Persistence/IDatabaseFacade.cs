using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DrugStore.Persistence;

public interface IDatabaseFacade
{
    public DatabaseFacade Database { get; }
}