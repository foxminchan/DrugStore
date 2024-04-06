using Ardalis.Specification;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Persistence.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot;