using Ardalis.Result;

using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string? FullName,
    string? Phone,
    Address? Address) : ICommand<Result<Guid>>;
