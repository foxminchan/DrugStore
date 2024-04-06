using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed record CreateUserCommand(
    Guid RequestId,
    string Email,
    string Password,
    string ConfirmPassword,
    string? FullName,
    string? Phone,
    Address? Address,
    bool IsAdmin) : IdempotencyCommand<Result<IdentityId>>(RequestId);