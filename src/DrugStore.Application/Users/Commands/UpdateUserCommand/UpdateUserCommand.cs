using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed record UpdateUserCommand(
    IdentityId Id,
    string Email,
    string OldPassword,
    string Password,
    string ConfirmPassword,
    string? FullName,
    string? Phone,
    Address? Address) : ICommand<Result<UserVm>>;