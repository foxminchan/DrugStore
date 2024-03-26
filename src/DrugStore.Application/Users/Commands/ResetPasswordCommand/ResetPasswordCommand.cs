using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.ResetPasswordCommand;

public sealed record ResetPasswordCommand(IdentityId Id) : ICommand<Result<IdentityId>>;