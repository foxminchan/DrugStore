using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.UpdateUserInfoCommand;

public sealed record UpdateUserInfoCommand(
    IdentityId Id,
    string Email,
    string? FullName,
    string? Phone,
    Address? Address,
    string? Role) : ICommand<Result<UserVm>>;