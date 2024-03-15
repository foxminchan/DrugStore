using Ardalis.Specification;

namespace DrugStore.Domain.SharedKernel;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;