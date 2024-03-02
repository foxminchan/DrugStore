using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.DeleteUserCommand;

public sealed record DeleteUserCommand(IdentityId Id) : ICommand<Result>;