using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Users.Commands.DeleteUserCommand;

public sealed record DeleteUserCommand(IdentityId Id) : ICommand<Result>;