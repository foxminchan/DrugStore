using Ardalis.Specification;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Persistence.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;