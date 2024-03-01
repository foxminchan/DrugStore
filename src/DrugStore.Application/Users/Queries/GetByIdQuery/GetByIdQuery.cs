using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<UserVm>>;