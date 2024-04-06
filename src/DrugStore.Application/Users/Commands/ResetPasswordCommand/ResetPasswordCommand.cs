using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Users.Commands.ResetPasswordCommand;

public sealed record ResetPasswordCommand(IdentityId Id) : ICommand<Result<IdentityId>>;