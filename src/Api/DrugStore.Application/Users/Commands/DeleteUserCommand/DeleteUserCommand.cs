using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.DeleteUserCommand;

public sealed record DeleteUserCommand(Guid Id) : ICommand<Result>;
