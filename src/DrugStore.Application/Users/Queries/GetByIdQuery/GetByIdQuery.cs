using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Users.Queries.GetByIdQuery;

public sealed record GetByIdQuery(IdentityId Id) : IQuery<Result<UserVm>>;