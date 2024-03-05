using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.Application.Users.ViewModels;

public sealed record UserVm(
    IdentityId Id,
    string? Email,
    string? FullName,
    string? Phone,
    Address? Address
);