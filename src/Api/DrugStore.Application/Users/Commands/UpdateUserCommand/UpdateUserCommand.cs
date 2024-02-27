using Ardalis.Result;
using DrugStore.Application.Users.ViewModel;
using DrugStore.Domain.Identity;
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
