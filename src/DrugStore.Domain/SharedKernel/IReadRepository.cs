using Ardalis.Specification;

namespace DrugStore.Domain.SharedKernel;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot;