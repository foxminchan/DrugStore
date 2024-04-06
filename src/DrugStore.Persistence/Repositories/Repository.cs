using Ardalis.Specification.EntityFrameworkCore;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Persistence.Repositories;

public sealed class Repository<T>(ApplicationDbContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;