using Ardalis.Specification.EntityFrameworkCore;

namespace DrugStore.Persistence;

public sealed class Repository<T>(ApplicationDbContext dbContext) : RepositoryBase<T>(dbContext)
    where T : class;
