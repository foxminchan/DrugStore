using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed record UpdateUserCommand(
    Guid Id,
    string Email,
    string Password,
    string ConfirmPassword,
    string? Role,
    string? FullName,
    string? Phone,
    Address? Address) : ICommand<Result<UserVm>>;